using BookTour.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Domain.Interface
{
    public interface IImageRepository
    {
        Task<List<Image>> GetImageByDetailRouteIdAsync(int detailRouteId);
        Task<bool> InsertAsync(Image image);
        Task<bool> UpdateAsync(Image image);
        Task<bool> DeleteAsync(int id);

    }
}
