using BookTour.Application.Dto;
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
        Task<Page<User>> GetAllUserAsync(int page, int size);

        Task<User> AddUser();

        Task<UserDTO> Login(LoginRequestDTO request);

        TokenInfo GenerateToken(User user); 

        Task<List<User>> getListUser();
    }
}
