using Gym_Management_System.Data;
using Gym_Management_System.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace Gym_Management_System.DataAccess
{
    public class MembershipData
    {

        public readonly AppDbContext _context;
        public MembershipData(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> HasActiveMembershipAsync(int memberID)
        {
            bool result = await _context.Memberships.AsNoTracking().AnyAsync(m => m.MemberId == memberID && m.Status == "Active" && m.EndDate > DateTime.Now);
            return result;
        }

        public async Task ExpireMembershipsAsync()
        {
            var expiredMemberships = await _context.Memberships.Where(m => m.Status == "Active" && m.EndDate <= DateTime.Now).ToListAsync();

            foreach (var item in expiredMemberships)
            {
                item.Status = "Expired";
            }

            await _context.SaveChangesAsync();
        }


        public async Task<int> AddNewMembershipAsync(Membership membership)
        {

            await _context.Memberships.AddAsync(membership);

            await _context.SaveChangesAsync();

            return membership.MembershipId;
        }

        public async Task<(DateTime StartDate, DateTime EndDate, string FirstName, string LastName)?> FindMembershipByPhoneAsync(string Phone) // Tuple
        {
            var MembershipDate = await _context.Memberships.AsNoTracking().Where(m => m.Status == "Active" && m.Member.Person.Phone == Phone).Select(m => new
            {
                m.StartDate,
                m.EndDate,
                m.Member.Person.FirstName,
                m.Member.Person.LastName
            }).FirstOrDefaultAsync();

            if (MembershipDate == null)
                return null;

            return (MembershipDate.StartDate, MembershipDate.EndDate, MembershipDate.FirstName, MembershipDate.LastName);
        }

        public async Task<List<Membership>> GetActiveMembershipsAsync()
        {
            var memberships = await _context.Memberships.AsNoTracking().Where(m => m.Status == "Active").Include(m => m.Member).ThenInclude(m => m.Person).Include(m => m.SubscriptionType).ToListAsync();

            return memberships;
        }

        public async Task<Membership?> GetMembershipByMembershipIDAsync(int MembershipID)
        {
            var membership = await _context.Memberships.AsNoTracking().Where(m => m.MembershipId == MembershipID).Include(m => m.Member).ThenInclude(m => m.Person).Include(m => m.SubscriptionType).FirstOrDefaultAsync();

            if (membership == null)
                return null;

            return membership;
        }

        public async  Task<List<Membership>> GetMembershipByMemberIDAsync(int memberId)
        {
            var memberships = await _context.Memberships.AsNoTracking().Where(m => m.MemberId == memberId).Include(m => m.Member).ThenInclude(m => m.Person).Include(m => m.SubscriptionType).ToListAsync();

            return memberships;
        }

    }
}
