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
    public class ArrivalRepository : IArrivalRepository
    {
        private readonly BookTourDbContext _dbContext;
        public ArrivalRepository(BookTourDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Arrival>> getAllArrivalAsync()
        {
            var query = _dbContext.Arrivals
              .Include(arrival => arrival.Routes)
                .ThenInclude(route => route.Detailroutes);  
            return await query.ToListAsync();
        }
    }
}
