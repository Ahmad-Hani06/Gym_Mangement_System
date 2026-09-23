using Gym_Management_System.Data;
using Gym_Management_System.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;

namespace Gym_Management_System.DataAccess
{
    public class SubscriptionTypeData
    {

        private readonly AppDbContext _context;
        public SubscriptionTypeData(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SubscriptionType>> GetAllSubscriptionTypesAsync()
        {
            var subscriptionTypes = await _context.SubscriptionTypes.AsNoTracking().ToListAsync();
            

            return subscriptionTypes;
        }

        public async Task<int?> GetDurationMonthsAsync(int SubscriptionTypeID)
        {
            int durationMonths = await _context.SubscriptionTypes.AsNoTracking()
                .Where(type => type.SubscriptionId == SubscriptionTypeID).Select(s => s.DurationMonths).FirstOrDefaultAsync();

            if (durationMonths == 0)
                return null;

            return durationMonths;
        }


        public async Task<decimal> GetSubscriptionTypePriceAsync(int SubscriptionTypeID)
        {
            decimal price = await _context.SubscriptionTypes.AsNoTracking().Where(s => s.SubscriptionId == SubscriptionTypeID).Select(s => s.Price).FirstOrDefaultAsync();

            return price;
        }

        public async Task<bool> UpdateSubscriptionTypeAsync(SubscriptionType subscriptionType)
        {
            var currentSubscriptionType = await _context.SubscriptionTypes.
                Where(s => s.SubscriptionId == subscriptionType.SubscriptionId).FirstOrDefaultAsync();


            if (currentSubscriptionType == null)
                return false;

            currentSubscriptionType.Name = subscriptionType.Name;
            currentSubscriptionType.DurationMonths = subscriptionType.DurationMonths;
            currentSubscriptionType.Price = subscriptionType.Price;


            await _context.SaveChangesAsync();

            return true;
        }


    }
}
