using BookTour.Domain.Entity;
using BookTour.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class ImageRepository : IImageRepository
    {
        public Task<List<Image>> GetImageByDetailRouteIdAsync(int detailRouteId)
        {
            throw new NotImplementedException();
        }
    }
}
