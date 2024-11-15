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
    public class LegRepository : ILegRepository
    {
        private readonly BookTourDbContext _dbContext;
        public LegRepository(BookTourDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Leg>> GetAllLegByDetailRouteIdAsync(int detailRouteId)
        {
            var query = await _dbContext.Legs
                .Where(leg => leg.DetailRouteId == detailRouteId)
                .ToListAsync();

            return query;
        }
    }
}
