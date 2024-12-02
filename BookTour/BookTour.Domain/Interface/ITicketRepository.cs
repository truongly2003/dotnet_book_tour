﻿using BookTour.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Domain.Interface
{
    public interface ITicketRepository
    {
        Task<List<Ticket>> GetAllTicketByUserIdAsync(int customerId, int BookingId);
        Task SaveAsync(Ticket ticket);
    }
}
