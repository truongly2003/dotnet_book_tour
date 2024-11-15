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
                .Select(detail => new Detailroute
                {
                    DetailRouteId = detail.DetailRouteId,
                    RouteId = detail.RouteId,
                    Price = detail.Price,
                    DetailRouteName = detail.DetailRouteName,
                    Description = detail.Description,
                    TimeToDeparture = detail.TimeToDeparture,
                    TimeToFinish = detail.TimeToFinish,
                    Stock = detail.Stock,
                    Images = detail.Images.Where(img => img.IsPrimary == 1).ToList(),
                    Feedbacks = detail.Feedbacks.ToList(),
                })
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
            .Select(detail => new Detailroute
            {
                DetailRouteId = detail.DetailRouteId,
                RouteId = detail.RouteId,
                Price = detail.Price,
                DetailRouteName = detail.DetailRouteName,
                Description = detail.Description,
                TimeToDeparture = detail.TimeToDeparture,
                TimeToFinish = detail.TimeToFinish,
                Stock = detail.Stock,
                Images = detail.Images.Where(img => img.IsPrimary == 1).ToList(),
                Feedbacks = detail.Feedbacks.ToList(),
            })
            .ToListAsync();
            return query;
        }

        public async Task<List<Detailroute>> GetAllRouteByArrivalNameAsync(string ArrivalName)
        {
            var query = await _dbContext.Detailroutes
               .Include(detail => detail.Images)
               .Include(detail => detail.Feedbacks)
               .Where(detail => detail.Route.Arrival.ArrivalName == ArrivalName)
               .Select(detail => new Detailroute
               {
                   DetailRouteId = detail.DetailRouteId,
                   RouteId = detail.RouteId,
                   Price = detail.Price,
                   DetailRouteName = detail.DetailRouteName,
                   Description = detail.Description,
                   TimeToDeparture = detail.TimeToDeparture,
                   TimeToFinish = detail.TimeToFinish,
                   Stock = detail.Stock,
                   Images = detail.Images.Where(img => img.IsPrimary == 1).ToList(),
                   Feedbacks = detail.Feedbacks.ToList(),
               })
               .ToListAsync();
            return query;
        }

        public Task<Detailroute> GetDetailRouteByIdAsync(int DetailRouteId)
        {
            throw new NotImplementedException();
        }
    }
}
