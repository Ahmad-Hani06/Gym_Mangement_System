using Gym_Management_System.Data;
using Gym_Management_System.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym_Management_System.DataAccess
{
    public class CoachData
    {


        private readonly AppDbContext _context;
        public CoachData(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddCoach(Coach coach)
        {
            await _context.Coaches.AddAsync(coach);
            await _context.SaveChangesAsync();

            return coach.CoachId;
        }

        public async Task<bool> IsCoachExistsByPersonId(int id)
        {
            return await _context.Coaches.AnyAsync(c => c.PersonId == id);
        }



    }
}
