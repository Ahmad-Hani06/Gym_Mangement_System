using Gym_Management_System.Business.DTOs;
using Gym_Management_System.DataAccess;
using Gym_Management_System.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym_Management_System.Business.Services
{
    public class CoachService
    {
        private readonly CoachData _coachData;
        private readonly PersonService _personService;
        public CoachService(CoachData coachData, PersonService personService)
        {
            _coachData = coachData;
            _personService = personService;
        }

        public async Task<(int CoachId, string Message)> AddCoach(CreateCoachDto coachDto)
        {
            // Check if person exists in Persons table 
            if (!await _personService.CheckIfPersonExistsByIdAsync(coachDto.PersonId))
                return (-1, "Person not found.");

            // Check if person exists in Coahes table
            if (await _coachData.IsCoachExistsByPersonId(coachDto.PersonId))
                return (-1, "This person is already Coach.");

            Coach coach = new Coach()
            {
                PersonId = coachDto.PersonId,
                HireDate = DateTime.Now,
                IsActive = true,
                Notes = coachDto.Notes
            };


            int coachId = await _coachData.AddCoach(coach);

            return (coachId, "Coach added successfully");
        }

        public async Task<List<CoachResponseDto>> GetAllCoaches()
        {
            var coaches = await _coachData.GetAllCoaches();

            List<CoachResponseDto> coachResponseDtos = new List<CoachResponseDto>();

            foreach (var coach in coaches)
            {
                CoachResponseDto coachDto = new CoachResponseDto()
                {
                    CoachId = coach.CoachId,
                    PersonId = coach.PersonId,
                    FirstName = coach.Person.FirstName,
                    LastName = coach.Person.LastName,
                    Phone = coach.Person.Phone,
                    HireDate = coach.HireDate,
                    IsActive = coach.IsActive ? "Active" : "Inactive",
                    Notes = coach.Notes
                };

                coachResponseDtos.Add(coachDto);
            }

            return coachResponseDtos;
        }
    }
}
