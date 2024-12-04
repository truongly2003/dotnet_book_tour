using BookTour.Application.Dto;
using BookTour.Application.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Interface
{
    public interface IDetailRouteService
    {
        Task<Page<DetailRouteResponse>> GetAllDetailRouteAsync(int page, int size);
        Task<DetailRouteResponse> GetDetailRouteByIdAsync(int id);
        Task<bool> InsertAsync(DetailRouteRequest d);
        Task<bool> UpdateAsync(int id, DetailRouteRequest d);
        Task<bool> DeleteAsync(int id);
        Task<bool> CheckExistAsync(int id);
    }
}
