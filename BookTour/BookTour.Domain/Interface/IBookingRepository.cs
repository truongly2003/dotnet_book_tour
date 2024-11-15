﻿using BookTour.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Domain.Interface
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAllBookingByCustomerIdAsync(int CustomerId);
        Task<Booking> findById(int id);
    }
}
