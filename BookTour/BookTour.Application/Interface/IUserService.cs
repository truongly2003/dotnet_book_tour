using BookTour.Application.Dto;
using BookTour.Application.Dto.Request;
using BookTour.Domain.Entity;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Interface
{
    public interface IUserService
    {
        Task<Page<UserDTO>> GetAllUserAsync(int page, int size);

        Task<User> AddUser(UserCreateRequest request);


        TokenInfo GenerateToken(User user); 

        Task<List<User>> getListUser();
         Task<UserDTO> UpdateUserAsync(UserUpdateRequest request, int id);
        Task blockUser(int userId ,int status);
        public Task<bool> ExistsByEmailAsync(string email);

        Task SendVerificationEmailAsync(User user);
        Task VerifyEmail(string token);
        Task<UserDTO> GetUserById(int id);
    }
}
