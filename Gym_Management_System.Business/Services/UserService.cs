using Gym_Management_System.Business.DTOs;
using Gym_Management_System.DataAccess;
using Gym_Management_System.Entities;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;

namespace Gym_Management_System.Business.Services
{
    public class UserService
    {

        private readonly UserData _userData;
        private readonly PersonService _personService;
        public UserService(UserData userData, PersonService personService)
        {
            _userData = userData;
            _personService = personService;
        }

        public async Task<(int UserID, string Message)> AddUserAsync(CreateUserDto userDto)
        {

            if (!await _personService.CheckIfPersonExistsByIdAsync(userDto.PersonId))
            {
                return (-1, "Person not found.");
            }

            if (await _userData.IsUserExistsByPersonIdAsync(userDto.PersonId))
            {
                return (-1, "This person is already User.");

            }

            User user = new User()
            {
                PersonId = userDto.PersonId,
                UserName = userDto.UserName,
                PasswordHash = PasswordHashing.ComputeHash(userDto.Password), 
                Role = "Admin",
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            int UserId = await _userData.AddUserAsync(user);

            return (UserId, "Added User Successfully");
        }

        public async Task<bool> UpdateUserStatus(int userId, string status)
        {

            if(!await _userData.IsUserExistsByUserIdAsync(userId))
            {
                return false;
            }

            bool isActive;

            if (status.ToLower() == "active")
                isActive = true;

            else if (status.ToLower() == "inactive")
                isActive = false;

            else
                throw new ArgumentException("Status must be 'active' or 'inactive'.");


            bool result = await _userData.UpdateUserStatusAsync(userId, isActive);

            if (!result)
                return false;

            return result;
        }
        public async Task<List<UserResponseDto>> GetAllUsersAsync()
        {
            var usersList = await _userData.GetAllUsersAsync();
            List<UserResponseDto> responseDto = new List<UserResponseDto>();

            foreach (var user in usersList)
            {
                UserResponseDto userResponse = new UserResponseDto()
                {
                    UserId = user.UserId,
                    PersonId = user.PersonId,
                    FirstName = user.Person.FirstName,
                    LastName = user.Person.LastName,
                    UserName = user.UserName,
                    Role = user.Role,
                    IsActive = user.IsActive,
                    CreatedAt = user.CreatedAt
                };
                responseDto.Add(userResponse);
            }

            return responseDto;
        }

        public async Task<UserResponseDto?> GetUserByUserIdAsync(int userId)
        {
            var user = await _userData.GetUserByUserIdAsync(userId);

            if (user == null)
                return null;

            UserResponseDto userResponseDto = new UserResponseDto()
            {
                UserId = user.UserId,
                PersonId = user.PersonId,
                FirstName = user.Person.FirstName,
                LastName = user.Person.LastName,
                UserName = user.UserName,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };

            return userResponseDto;

        }


        public async Task<bool> ChangeUserPasswordAsync(int id, string password)
        {
            if (!await _userData.IsUserExistsByUserIdAsync(id))
            {
                return false;
            }

            bool result = await _userData.ChangeUserPasswordAsync(id, PasswordHashing.ComputeHash(password));
            return result;
        }
        public async Task<bool> ChangeUserNameAsync(int id, string UserName)
        {
            if (!await _userData.IsUserExistsByUserIdAsync(id))
            {
                return false;
            }

            bool result = await _userData.ChangeUserUserNameAsync(id,UserName);
            return result;
        }
    }
}
