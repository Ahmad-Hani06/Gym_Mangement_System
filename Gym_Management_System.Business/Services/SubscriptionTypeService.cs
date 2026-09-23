using Gym_Management_System.Business.DTOs;
using Gym_Management_System.Data;
using Gym_Management_System.DataAccess;
using Gym_Management_System.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Gym_Management_System.Business.Services
{
    public class SubscriptionTypeService
    {

        private  SubscriptionTypeData _subscriptionTypeData;
        public SubscriptionTypeService(SubscriptionTypeData subscriptionTypeData)
        {
            _subscriptionTypeData = subscriptionTypeData;
        }

        public async Task<List<SubscriptionTypeDto>> GetAllSubscriptionTypeAsync()
        {
            var subscriptionTypesList = await _subscriptionTypeData.GetAllSubscriptionTypesAsync();

            var subscriptionTypesDto = subscriptionTypesList.Select(type => new SubscriptionTypeDto
            {
                SubscriptionId = type.SubscriptionId,
                Name = type.Name,
                DurationMonths = type.DurationMonths,
                price = type.Price
            }).ToList();


            return subscriptionTypesDto;
        }

        public async Task<bool> UpdateSubscriptionType(int id, SubscriptionTypeDto subscriptionTypeDto)
        {
            SubscriptionType subscriptionType = new SubscriptionType()
            {
                SubscriptionId = id,
                Name = subscriptionTypeDto.Name,
                DurationMonths = subscriptionTypeDto.DurationMonths,
                Price = subscriptionTypeDto.price
            };

            bool result = await _subscriptionTypeData.UpdateSubscriptionTypeAsync(subscriptionType);

            if (!result)
                return false;

            return true;
        }
    }

}
