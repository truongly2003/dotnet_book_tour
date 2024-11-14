using BookTour.Domain.Entity;
using BookTour.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class DepartureRepository : IDepartureRepository
    {
        private readonly BookTourDbContext _dbContext;
        public DepartureRepository(BookTourDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Departure>> getAllDepartureAsync()
        {
            return await _dbContext.Departures.ToListAsync();
        }
    }
}
