using BookTour.Application.Dto;
using BookTour.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Service
{
    public class ImageService : IImageService 
    {
        public Task<List<ImageDTO>> GetImageByDetailRouteIdAsync(int detailRouteId)
        {
            throw new NotImplementedException();
        }
    }
}
