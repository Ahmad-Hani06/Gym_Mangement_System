using Gym_Management_System.Business.DTOs;
using Gym_Management_System.DataAccess;
using Gym_Management_System.Entities;

namespace Gym_Management_System.Business.Services
{
    public class PersonService
    {
        private readonly PersonData _personData;
        public PersonService(PersonData personData)
        {
            _personData = personData;
        }

        public async Task<PersonDto?> CheckIfPersonExistsByNameAsync(string fName, string lName)
        {
            var person = await _personData.CheckIfPersonExistsByNameAsync(fName, lName);

            if (person == null)
                return null;

            PersonDto personDto = new PersonDto()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Phone = person.Phone,
                Gender = person.Gender
            };

            return personDto;
            
        }


        public async Task<bool> CheckIfPersonExistsByIdAsync(int Id)
        {
            var result = await _personData.CheckIfPersonExistsByIdAsync(Id);
            return result;
        }

        public async Task<int> AddPersonAsync(PersonDto personDto)
        {
            Person person = new Person()
            {
                FirstName = personDto.FirstName,
                LastName = personDto.LastName,
                Phone = personDto.Phone,
                Gender = personDto.Gender
            };

            int personID = await _personData.AddPersonAsync(person);

            return personID;
        }


        

    }
}
