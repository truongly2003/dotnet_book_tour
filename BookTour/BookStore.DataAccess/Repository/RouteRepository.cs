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
    public class RouteRepository : IRouteRepository
    {
        private readonly BookTourDbContext _dbContext;
        public RouteRepository(BookTourDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Detailroute>> GetAllRouteAsync()
        {
            var query = await _dbContext.Detailroutes
                .Include(detail => detail.Images)
                .Include(detail => detail.Feedbacks)
                .ToListAsync();
            return query;
        }

        public async Task<List<Detailroute>> GetAllRouteByArrivalAndDepartureAndDateAsync(string ArrivalName, string DepartureName, DateOnly TimeToDeparture)
        {
            var curendate = DateOnly.FromDateTime(DateTime.Now);
            var query = await _dbContext.Detailroutes
            .Include(detail => detail.Images)
            .Include(detail => detail.Feedbacks)
            .Where(detail => detail.Route.Arrival.ArrivalName == ArrivalName &&
                            detail.Route.Departure.DepartureName == DepartureName &&
                            detail.TimeToDeparture >= TimeToDeparture && detail.TimeToDeparture >= curendate
            )
          
            .ToListAsync();
            return query;
        }
        public async Task<List<Detailroute>> GetAllRouteByArrivalNameAsync(string ArrivalName)
        {
            var query = await _dbContext.Detailroutes
               .Include(detail => detail.Images)
               .Include(detail => detail.Feedbacks)
               .Where(detail => detail.Route.Arrival.ArrivalName == ArrivalName)
               .ToListAsync();
            return query;
        }
        public async Task<Detailroute> GetDetailRouteByIdAsync(int DetailRouteId)
        {
            var query = await _dbContext.Detailroutes
             .Include(detail => detail.Images)
             .Include(detail => detail.Feedbacks)
             .Include(detail=>detail.Route)
             .ThenInclude(route=>route.Departure)
             .Where(detail => detail.DetailRouteId==DetailRouteId)
             .Select(detail => new Detailroute
             {
                
                 Route=new Route
                 {
                     Departure=new Departure
                     {
                         DepartureName=detail.Route.Departure.DepartureName
                     }
                 }
             })
             .FirstOrDefaultAsync();
            return query;
        }
    }
}
