﻿using BookTour.Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Interface
{
    public interface IDepartureService
    {
        Task<List<DepartureDTO>> getAllDepartureAsync();
    }
}
