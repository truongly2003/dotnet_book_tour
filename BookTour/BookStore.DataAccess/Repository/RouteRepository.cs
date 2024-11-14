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

        //var query = await _dbContext.Detailroutes
        //     .Join(_dbContext.Images, detail => detail.DetailRouteId, img => img.DetailRouteId, (detail, img) => new { detail, img })
        //     .Where(x => x.img.IsPrimary==1)
        //     .Select(x => new Detailroute
        //     {
        //         DetailRouteId = x.detail.DetailRouteId,
        //         DetailRouteName = x.detail.DetailRouteName,
        //         RouteId = x.detail.RouteId,
        //         Description = x.detail.Description,
        //         Stock = x.detail.Stock,
        //         TimeToDeparture = x.detail.TimeToDeparture,
        //         TimeToFinish = x.detail.TimeToFinish,
        //         Price = x.detail.Price,
        //         Images = _dbContext.Images
        //        .Where(img => img.DetailRouteId == x.detail.DetailRouteId && img.IsPrimary == 1)
        //        .Select(img => new BookTour.Domain.Entity.Image
        //        {
        //            ImageId = img.ImageId,
        //            TextImage = img.TextImage,
        //        })
        //        .ToList()
        //     }).ToListAsync();
        //return query;
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
                    Images = detail.Images.Where(img=>img.IsPrimary==1).ToList(),
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
               .Where(detail=>detail.Route.Arrival.ArrivalName==ArrivalName)
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
            Console.WriteLine("hello" + query);
            return query;
        }
    }
}
