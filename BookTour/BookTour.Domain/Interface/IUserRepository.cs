using BookTour.Domain.Entity;
using System;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Domain.Interface
{
    public interface IUserRepository
    {
        Task<User> findByUsername(string username);
        Task<bool> existsByUsernameAsync(string username);

        Task<List<User>> FindAllByStatusAsync(int status);
        Task<User> saveUser(User user);
        Task<User> updateUser(User user);
        Task<User> checkUserExist(string email);
        Task<User> FindUserById(int id);
        Task<bool> ExistsByEmailAsync(string email);
    }
}
