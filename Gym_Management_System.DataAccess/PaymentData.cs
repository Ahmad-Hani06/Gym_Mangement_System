using Gym_Management_System.Data;
using Gym_Management_System.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym_Management_System.DataAccess
{
    public class PaymentData
    {
        private readonly AppDbContext _context;
        public PaymentData(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddPaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();

            return payment.MembershipId;
        }

        public async Task <List<Payment>> GetAllPaymentsByMemebrIDAsync(int MemberId)
        {
            var Payments = await _context.Payments.AsNoTracking().Include(p => p.Membership).
                ThenInclude(m => m.Member).ThenInclude(m => m.Person).Where(m => m.Membership.MemberId == MemberId).ToListAsync();

            return Payments;
        }

        public async Task<List<Payment>> GetPaymentsAsync()
        {
            var Payments = await _context.Payments.AsNoTracking().
                Include(p => p.Membership).ThenInclude(m => m.Member).ThenInclude(m => m.Person).ToListAsync();

            return Payments;
        }

    }
}
