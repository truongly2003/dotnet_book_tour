using BookTour.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class RouteRepository:IRouteRepository
    {
        private readonly BookTourDbContext _dbContext;
        public RouteRepository(BookTourDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
