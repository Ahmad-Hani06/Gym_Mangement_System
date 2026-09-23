using Gym_Management_System.Business.DTOs;
using Gym_Management_System.DataAccess;
using Gym_Management_System.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym_Management_System.Business.Services
{
    public class PaymentService
    {

        private readonly PaymentData _paymentData;
        public PaymentService(PaymentData paymentData)
        {
            _paymentData = paymentData;
        }

        public async Task<int> AddPaymentAsync(Payment payment)
        {
            int paymentId = await _paymentData.AddPaymentAsync(payment);

            return paymentId;
        }


        public async Task<List<PaymentResponseDto>> GetAllPaymentsByMemebrIDAsync(int memberID)
        {
            var PaymentsList = await _paymentData.GetAllPaymentsByMemebrIDAsync(memberID);

            List<PaymentResponseDto> paymentResponsesDto = new List<PaymentResponseDto>();

            if (PaymentsList.Count == 0)
                return paymentResponsesDto; // Zero


                foreach (var Payment in PaymentsList)
                {
                    PaymentResponseDto paymentResponseDto = new PaymentResponseDto()
                    {
                        PaymentId = Payment.PaymentId,
                        MembershipId = Payment.MembershipId,
                        MemberName = Payment.Membership.Member.Person.FirstName + " " + Payment.Membership.Member.Person.LastName,
                        Amount = Payment.Amount,
                        PaymentDate = Payment.PaymentDate
                    };

                    paymentResponsesDto.Add(paymentResponseDto);
                }


            return paymentResponsesDto;
        }

        public async Task <List<PaymentResponseDto>> GetAllPaymentsAsync()
        {
            var PaymentsList = await _paymentData.GetPaymentsAsync();

            List<PaymentResponseDto> paymentResponsesDto = new List<PaymentResponseDto>();

            if (PaymentsList.Count == 0)
                return paymentResponsesDto; // Zero


            foreach (var Payment in PaymentsList)
            {
                PaymentResponseDto paymentResponseDto = new PaymentResponseDto()
                {
                    PaymentId = Payment.PaymentId,
                    MembershipId = Payment.MembershipId,
                    MemberName = Payment.Membership.Member.Person.FirstName + " " + Payment.Membership.Member.Person.LastName,
                    Amount = Payment.Amount,
                    PaymentDate = Payment.PaymentDate
                };

                paymentResponsesDto.Add(paymentResponseDto);
            }


            return paymentResponsesDto;
        }

    }
}
