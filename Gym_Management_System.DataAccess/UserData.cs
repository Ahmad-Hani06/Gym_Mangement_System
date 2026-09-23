using Gym_Management_System.Data;
using Gym_Management_System.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym_Management_System.DataAccess
{
    public class UserData
    {

        public readonly AppDbContext _context;
        public UserData(AppDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user.UserId;
        }

        public async Task<bool> IsUserExistsByPersonIdAsync(int personId)
        {
            bool result = await _context.Users.AsNoTracking().Where(u => u.PersonId == personId).AnyAsync();
            return result;
        }

        public async Task<bool> UpdateUserStatusAsync(int UserId,bool isActive)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == UserId);

            if (user == null)
                return false;

            user.IsActive = isActive;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await _context.Users.AsNoTracking().Include(u => u.Person).ToListAsync();

            return users;
        }

    }
}
