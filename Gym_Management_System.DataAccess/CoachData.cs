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

        public async Task<List<Coach>> GetAllCoaches()
        {
            return await _context.Coaches.AsNoTracking().Include(c => c.Person).ToListAsync();
        }

        public async Task<Coach?> GetCoachById(int id)
        {
            return await _context.Coaches.AsNoTracking().Include(c => c.Person).FirstOrDefaultAsync(c => c.CoachId == id);
        }

        public async Task<bool> UpdateCoachActvityStatus(int id, bool status)
        {
           var coach = await _context.Coaches.Where(c => c.CoachId == id).FirstOrDefaultAsync();
    
            if (coach == null) 
                 return false;

            coach.IsActive = status;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> IsCoachExistsByCoachId(int id)
        {
            return await _context.Coaches.AnyAsync(c => c.CoachId == id);
        }

    }
}
