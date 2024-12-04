using BookTour.Application.Dto;
using BookTour.Application.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Interface
{
    public interface IImageService
    {
        Task<List<ImageDTO>> GetImageByDetailRouteIdAsync(int detailRouteId);
        //Task<bool> InsertAsync(ImageRequest i);
        //Task<bool> UpdateAsync(ImageRequest i);
    }
}
