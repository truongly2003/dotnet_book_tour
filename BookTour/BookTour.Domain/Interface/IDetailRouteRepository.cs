using BookTour.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Domain.Interface
{
    public interface IDetailRouteRepository
    {
        Task<Detailroute> findById(int id);
        Task<List<Detailroute>> GetAllDetailRouteAsync();
        Task<Detailroute> GetDetailRouteByIdAsync(int id);
        Task<bool> InsertAsync(Detailroute detailroute);
        Task<bool> UpdateAsync(Detailroute detailroute);
        Task<bool> DeleteAsync(int id);
    }
}
