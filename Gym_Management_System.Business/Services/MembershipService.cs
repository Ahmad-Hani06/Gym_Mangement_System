using Gym_Management_System.Business.DTOs;
using Gym_Management_System.DataAccess;
using Gym_Management_System.DataAccess.Members;
using Gym_Management_System.Entities;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace Gym_Management_System.Business.Services
{
    public class MembershipService
    {

        private readonly MembershipData _membershipData;
        private readonly SubscriptionTypeData _subscriptionTypeData;
        private readonly MemberData _memberData;
        private readonly PaymentService _paymentService;
        public MembershipService(MembershipData membershipData, SubscriptionTypeData subscriptionTypeData, MemberData memberData, PaymentService paymentService)
        {
            _membershipData = membershipData;
            _subscriptionTypeData = subscriptionTypeData;
            _memberData = memberData;
            _paymentService = paymentService;
        }

        private async Task<MembershipResponseDto> AddNewMembership(MembershipDto membershipDto, int durationMonths, decimal Price)
        {

            DateTime startDate = DateTime.Now;
            Membership membership = new Membership() // I will Update UserID here 
            {
                MemberId = membershipDto.MemberId,
                SubscriptionTypeId = membershipDto.SubscriptionTypeId,
                StartDate = startDate,
                EndDate = startDate.AddMonths(durationMonths),
                Price = Price,
                Status = "Active",
                Notes = membershipDto.Notes,
                CreatedByUserId = 1
            };


            int membershipID = await _membershipData.AddNewMembershipAsync(membership);

            return new MembershipResponseDto {
                MembershipId = membership.MembershipId,
                MemberId = membership.MemberId,
                SubscriptionTypeId = membership.SubscriptionTypeId,
                StartDate = membership.StartDate,
                EndDate = membership.EndDate,
                Price = membership.Price,
                Status = membership.Status
            };
            
        }

        private async Task<int> AddPayment(int memebrshipId,decimal price) // i will update UserID 
        {
            Payment payment = new Payment()
            {
                MembershipId = memebrshipId,
                CreatedByUserId = 1,
                Amount = price,
                PaymentDate = DateTime.Now,
                Notes = "Paid"
            };

            int paymentId = await _paymentService.AddPaymentAsync(payment);
            return paymentId;
        }
        public async Task<MembershipResponseDto> AddMembershipAsync(MembershipDto membershipDto)
        {

            using var transaction = await _membershipData._context.Database.BeginTransactionAsync();

            try
            {
                int? durationMonths = await _subscriptionTypeData.GetDurationMonthsAsync(membershipDto.SubscriptionTypeId);

                if (durationMonths == null)
                    throw new Exception("Subscription type not found.");

                if (durationMonths <= 0)
                    throw new Exception("Subscription duration must be greater than zero.");


                bool IsFound = await _memberData.IsMemberExistsAsync(membershipDto.MemberId);

                if (!IsFound)
                    throw new Exception("Member is not found.");


                bool HasActiveMembership = await _membershipData.HasActiveMembershipAsync(membershipDto.MemberId);

                if (HasActiveMembership)
                    throw new Exception("Member already has an active membership.");


                decimal Price = await _subscriptionTypeData.GetSubscriptionTypePriceAsync(membershipDto.SubscriptionTypeId);


                var ResponseDto = await AddNewMembership(membershipDto, durationMonths.Value, Price);

                await AddPayment(ResponseDto.MembershipId, Price);
                
                await transaction.CommitAsync();

                return ResponseDto;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }

          
            
        }

        public async Task ExpireMembershipsAsync()
        {
            await _membershipData.ExpireMembershipsAsync();
        }

        public async Task<MembershipDatesDto?> FindMembershipByPhoneAsync(string Phone)
        {
            var MembershipDate = await _membershipData.FindMembershipByPhoneAsync(Phone);

            if (MembershipDate == null)
                return null;

            MembershipDatesDto membershipDate = new MembershipDatesDto()
            {
                FirstName = MembershipDate.Value.FirstName,
                LastName = MembershipDate.Value.LastName,
                StartDate = MembershipDate.Value.StartDate,
                EndDate = MembershipDate.Value.EndDate
            };

            return membershipDate;
        }

        public async Task<List<ActiveMembershipResponseDto>> GetActiveMembershipsAsync()
        {
            var membershipsList = await _membershipData.GetActiveMembershipsAsync();

            List<ActiveMembershipResponseDto> membershipResponseDtoList = new List<ActiveMembershipResponseDto>();

            foreach (var memebrship in membershipsList) 
            {
                ActiveMembershipResponseDto activeMembershipResponse = new ActiveMembershipResponseDto()
                {
                    MembershipId = memebrship.MembershipId,
                    MemberId = memebrship.MemberId,
                    SubscriptionTypeID = memebrship.SubscriptionTypeId,
                    DurationMonths = memebrship.SubscriptionType.DurationMonths,
                    Price = memebrship.Price,
                    FirstName = memebrship.Member.Person.FirstName,
                    LastName = memebrship.Member.Person.LastName,
                    StartDate = memebrship.StartDate,
                    EndDate = memebrship.EndDate
                };
                membershipResponseDtoList.Add(activeMembershipResponse);
            }

            return membershipResponseDtoList;
        }

        public async Task<MembershipDetailsResponseDto?> GetMembershipByMembershipIDAsync(int Id)
        {
            var membership = await _membershipData.GetMembershipByMembershipIDAsync(Id);

            if (membership == null)
                return null;

            MembershipDetailsResponseDto responseDto = new MembershipDetailsResponseDto()
            {
                MembershipId = membership.MembershipId,
                MemberId = membership.MemberId,

                FirstName = membership.Member.Person.FirstName,
                LastName = membership.Member.Person.LastName,

                SubscriptionTypeID = membership.SubscriptionTypeId,
                SubscriptionTypeName = membership.SubscriptionType.Name,
                DurationMonths = membership.SubscriptionType.DurationMonths,

                Price = membership.SubscriptionType.Price,

                StartDate = membership.StartDate,
                EndDate = membership.EndDate,

                Status = membership.Status,
                Notes = membership.Notes
            };

            return responseDto;
        }

        public async Task<List<MembershipDetailsResponseDto>> GetMembershipByMemberIDAsync(int Id)
        {
            var memberships = await _membershipData.GetMembershipByMemberIDAsync(Id);

            List<MembershipDetailsResponseDto> membershipsResponse =
                new List<MembershipDetailsResponseDto>();

            foreach (var membership in memberships)
            {
                MembershipDetailsResponseDto responseDto =
                    new MembershipDetailsResponseDto()
                    {
                        MembershipId = membership.MembershipId,
                        MemberId = membership.MemberId,

                        FirstName = membership.Member.Person.FirstName,
                        LastName = membership.Member.Person.LastName,

                        SubscriptionTypeID = membership.SubscriptionTypeId,
                        SubscriptionTypeName = membership.SubscriptionType.Name,
                        DurationMonths = membership.SubscriptionType.DurationMonths,
                        Price = membership.SubscriptionType.Price,

                        StartDate = membership.StartDate,
                        EndDate = membership.EndDate,

                        Status = membership.Status,
                        Notes = membership.Notes
                    };

                membershipsResponse.Add(responseDto);
            }

            return membershipsResponse;
        }
    }
}
