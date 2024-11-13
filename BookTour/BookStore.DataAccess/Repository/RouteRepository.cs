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
    public class RouteRepository:IRouteRepository
    {
        private readonly BookTourDbContext _dbContext;
        public RouteRepository(BookTourDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Detailroute>> GetAllRouteAsync()
        {
            var query = await _dbContext.Detailroutes
                 .Join(_dbContext.Images, detail => detail.DetailRouteId, img => img.DetailRouteId, (detail, img) => new { detail, img })
                 .Where(x => x.img.IsPrimary==1)
                 .Select(x => new Detailroute
                 {
                     DetailRouteId = x.detail.DetailRouteId,
                     DetailRouteName = x.detail.DetailRouteName,
                     RouteId = x.detail.RouteId,
                     Description = x.detail.Description,
                     Stock = x.detail.Stock,
                     TimeToDeparture = x.detail.TimeToDeparture,
                     TimeToFinish = x.detail.TimeToFinish,
                     Price = x.detail.Price,
                     Images = _dbContext.Images
                    .Where(img => img.DetailRouteId == x.detail.DetailRouteId && img.IsPrimary == 1)
                    .Select(img => new BookTour.Domain.Entity.Image
                    {
                        ImageId = img.ImageId,
                        TextImage = img.TextImage,
                    })
                    .ToList()
                 }).ToListAsync();
            return query;
        }
    }
}
