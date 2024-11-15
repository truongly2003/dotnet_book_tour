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
    public class DetailRouteRepository : IDetailRouteRepository
    {
        private readonly BookTourDbContext _dbContext;
        public DetailRouteRepository(BookTourDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Detailroute> findById(int id)
        {
            return await _dbContext.Detailroutes.FirstOrDefaultAsync(d => d.DetailRouteId == id);
        }
    }
}
