﻿using BookTour.Application.Dto;
using BookTour.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Interface
{
    public interface IRoleService
    {
        public Task<List<RoleDTO>> getAllRole();
    }
}
