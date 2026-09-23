using Gym_Management_System.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Gym_Management_System.Entities;
using System.Net.WebSockets;
using Microsoft.EntityFrameworkCore;

namespace Gym_Management_System.DataAccess
{
    public class PersonData
    {
        private readonly AppDbContext _context;
        public PersonData(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddPersonAsync(Person person)
        {
            await _context.Persons.AddAsync(person);

            await _context.SaveChangesAsync();

            return person.PersonId;
        }


        public async Task<Person?> CheckIfPersonExistsByNameAsync(string FirstName, string LastName)
        {
            var person = await _context.Persons.Where(p => p.FirstName == FirstName && p.LastName == LastName).FirstOrDefaultAsync();

            if (person == null)
                return null;

            return person;
        }


        public async Task<bool> CheckIfPersonExistsByIdAsync(int Id)
        {
            var person = await _context.Persons.Where(p => p.PersonId == Id).FirstOrDefaultAsync();

            if (person == null)
                return false;

            return true;
        }
    }
}
