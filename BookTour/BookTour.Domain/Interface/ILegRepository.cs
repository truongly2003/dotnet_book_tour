﻿using BookTour.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Domain.Interface
{
    public interface ILegRepository
    {
        Task<List<Leg>> GetAllLegByDetailRouteIdAsync(int detailRouteId);
        Task<bool> InsertAsync(Leg leg);
        Task<bool> UpdateAsync(Leg leg);
        Task<bool> DeleteAsync(int id);
        Task<Leg> GetByIdAsync(int id);
    }
}
