using BookTour.Domain.Entity;
using BookTour.Domain.Interface;
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
        public Task<List<Leg>> GetAllLegByDetailRouteIdAsync(int detailRouteId)
        {
            throw new NotImplementedException();
        }
    }
}
