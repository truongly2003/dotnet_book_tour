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
        //public async Task<List<Arrival>> getAllArrivalAsync()
        //{
        //    //var query = _dbContext.Arrivals
        //    //     .Join(_dbContext.Routes, arrival => arrival.ArrivalId, route => route.ArrivalId, (arrival, route) => new { arrival, route })
        //    //     .Join(_dbContext.Detailroutes, ro => ro.route.RouteId, detail => detail.RouteId, (ro, detail) => new { ro.arrival, ro.route, detail })
        //    //     .Join(_dbContext.Images, r => r.detail.DetailRouteId, img => img.DetailRouteId, (r, img) => new { r.arrival, r.route, img })
        //    //     .Where(x => x.img.IsPrimary == 1)
        //    //     .GroupBy(x => x.arrival.ArrivalId)
        //    //     .Select(g => new Arrival
        //    //     {
        //    //         ArrivalId=g.Key,
        //    //         ArrivalName=g.FirstOrDefault().arrival.ArrivalName,
        //    //         Routes=_dbContext.Routes

        //    //     });
        //    //return await query.ToListAsync();




        //}
        //public async Task<List<Arrival>> getAllArrivalAsync()
        //{
        //    var query = _dbContext.Arrivals
        //        .Select(arrival => new Arrival
        //        {
        //            ArrivalId = arrival.ArrivalId,
        //            ArrivalName = arrival.ArrivalName,
        //            Routes = arrival.Routes.Select(route => new Route
        //            {
        //                Detailroutes = route.Detailroutes.Select(detail => new Detailroute
        //                {
        //                    Images = detail.Images
        //                        .Where(img => img.IsPrimary == 1)
        //                        .ToList()
        //                }).ToList()
        //            }).ToList()
        //        });

        //    return await query.ToListAsync();
        //}
        public async Task<List<Arrival>> getAllArrivalAsync()
        {
            var query = _dbContext.Arrivals
              .Include(arrival => arrival.Routes)
                .ThenInclude(route => route.Detailroutes);  
            return await query.ToListAsync();
        }

    }
}
