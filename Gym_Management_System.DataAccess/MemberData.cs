using Gym_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Gym_Management_System.Entities;

namespace Gym_Management_System.DataAccess.Members
{
    public class MemberData
    {
        public readonly AppDbContext _context;

        public MemberData(AppDbContext context)
        {
            _context = context;
        }


        public async Task<int> AddMemberAsync(Member member)
        {
            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();

            return member.MemberId;
        }

        public async Task<bool> IsMemberExistsAsync(int memberID)
        {
           bool isFound = await _context.Members.AnyAsync(m => m.MemberId == memberID);

            return isFound;
        }

        public async Task<List<Member>> GetAllMembersAsync()
        {
            var MembersList = await _context.Members.AsNoTracking().Include(m => m.Person).ToListAsync(); 

            return MembersList;
        }

        public async Task<Member?> GetMemberByIDAsync(int MemberID)
        {
            var Member = await _context.Members.AsNoTracking().Include(m => m.Person).FirstOrDefaultAsync(m=> m.MemberId == MemberID);

            return Member;
        }

        public async Task<bool> IsMemebrExistsByPersonId(int personId)
        {
            bool result = await _context.Members.AsNoTracking().Where(m => m.PersonId == personId).AnyAsync();

            return result;
        }








    }
}
