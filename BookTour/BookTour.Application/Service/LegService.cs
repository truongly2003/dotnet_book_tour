﻿using BookTour.Application.Dto;
using BookTour.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Service
{
    public class LegService : ILegService
    {
        public async Task<List<LegDTO>> GetAllLegByDetailRouteIdAsync(int detailRouteId)
        {
            throw new NotImplementedException();
        }
    }
}
