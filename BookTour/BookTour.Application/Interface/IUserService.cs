<<<<<<< HEAD
﻿using BookTour.Application.Dto;
using BookTour.Domain.Entity;
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
>>>>>>> 0f09373bbdf2c969964e6f7d9fd925b76a62a292
using System.Threading.Tasks;

namespace BookTour.Application.Interface
{
    public interface IUserService
    {
<<<<<<< HEAD
        Task<Page<User>> GetAllUserAsync(int page, int size);

        Task<User> AddUser();

        Task<UserDTO> Login(UserDTO request);

        Task<TokenInfo> GenerateToken(User user);  // Sửa thành TokenInfo
=======
>>>>>>> 0f09373bbdf2c969964e6f7d9fd925b76a62a292
    }
}
