﻿using Domain;
using Shared;
using Shared.UserDtos;

namespace DAL.Contracts.Interfaces
{
    public interface IUserRepository
    {
        Task<Response<User>> CreateUser(UserRegister dto);
        Task<Response> DeleteUser(string userId);
        Task<Response> UpdateUser(string userId, string newUsername, string newPassword);
        Task<User> GetUserById(string userId);
        Task<List<User>> GetAllUsers();
        Task<string?> GetUsernameById(string userId);
        Task<Response<User>> CheckPassword(UserLogin dto);
    }
}
