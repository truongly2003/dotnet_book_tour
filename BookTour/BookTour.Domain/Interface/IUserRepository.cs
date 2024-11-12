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

        Task<List<User>> findAllUser();
    }
}
