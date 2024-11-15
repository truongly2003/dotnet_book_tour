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
    public class ImageRepository : IImageRepository
    {
        private readonly BookTourDbContext _dbContext;
        public ImageRepository(BookTourDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Image>> GetImageByDetailRouteIdAsync(int detailRouteId)
        {
            var query =await _dbContext.Images
                .Where(img=>img.DetailRouteId == detailRouteId)
                .ToListAsync();
            return query;
        }
    }
}
