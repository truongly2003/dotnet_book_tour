﻿using BookTour.Application.Dto;
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

        Task<LoginDTO> Login(LoginRequestDTO request);

        TokenInfo GenerateToken(User user); 

        Task<List<User>> getListUser();
    }
}
