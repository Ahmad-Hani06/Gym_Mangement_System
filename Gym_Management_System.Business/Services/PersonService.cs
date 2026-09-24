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

        public async Task<(bool Deleted, string Message)> DeletePersonAsync(int personId)
        {
            bool exists = await _personData.CheckIfPersonExistsByIdAsync(personId);
            if (!exists)
                return (false, "Person not found.");

            bool hasRelatedRecords = await _personData.HasRelatedRecordsAsync(personId);

            if (hasRelatedRecords)
                return (false, "Person is linked to another record.");


            bool deleted = await _personData.DeletePersonByPersonId(personId);

            return deleted ? (true, "Person deleted successfully.") : (false, "Person could not be deleted.");

        }




    }
}
