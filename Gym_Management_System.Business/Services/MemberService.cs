using System;
using System.Collections.Generic;
using System.Text;
using Gym_Management_System.Business.DTOs;
using Gym_Management_System.DataAccess.Members;
using Gym_Management_System.Entities;

namespace Gym_Management_System.Business.Services
{
    public class MemberService
    {
        private readonly MemberData _MemberData;
        private readonly PersonService _personService;

        public MemberService(MemberData Member, PersonService personService)
        {
            _MemberData = Member;
            _personService = personService;
        }

        
        public async Task<(int MemberId, string Message)> RegisterMemberAsync(CreateMemberDto memberDto)
        {
            if (!await _personService.CheckIfPersonExistsByIdAsync(memberDto.PersonId))
            {
                return (-1,"Person not found.");
            }

            if (await _MemberData.IsMemebrExistsByPersonId(memberDto.PersonId))
            {
                return (-1,"This person is already registered as a member.");
            }

            Member member = new Member()
            {
                PersonId = memberDto.PersonId,
                JoinDate = DateTime.Now,
                Notes = memberDto.Notes
            };

            int memberID = await _MemberData.AddMemberAsync(member);

            return (memberID, "Member registered successfully.");
        }

        public async Task <List<MemberResponseDto>> GetllMemebrsAsync()
        {
            var Members = await _MemberData.GetAllMembersAsync();

            List<MemberResponseDto> memberResponse = new List<MemberResponseDto>();

            foreach (var member in Members)
            {
                MemberResponseDto memberResponseDto = new MemberResponseDto()
                {
                    MemberId = member.MemberId,
                    FirstName = member.Person.FirstName,
                    LastName = member.Person.LastName,
                    Gender = member.Person.Gender ? "Male" : "Female",
                    Phone = member.Person.Phone,
                    Notes = member.Notes,
                    JoinDate = member.JoinDate
                };

                memberResponse.Add(memberResponseDto);
            }

            return memberResponse;

        }

        public async Task<MemberResponseDto?> GetMemberByIDAcync(int MemberID)
        {
            var member = await _MemberData.GetMemberByIDAsync(MemberID);

            if (member == null)
            {
                return null;
            }

            return new MemberResponseDto 
            {
                MemberId = member.MemberId,
                FirstName = member.Person.FirstName,
                LastName = member.Person.LastName,
                Gender = member.Person.Gender ? "Male" : "Female",
                Phone = member.Person.Phone,
                Notes = member.Notes,
                JoinDate = member.JoinDate
            };



        }

    }
}
