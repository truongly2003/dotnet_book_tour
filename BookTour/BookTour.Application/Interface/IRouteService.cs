using BookTour.Application.Dto;
using BookTour.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Interface
{
    public interface IRouteService
    {
        Task<Page<RouteDTO>> GetAllRouteAsync(int page, int size, string sort);
        Task<RouteDTO> GetDetailRouteByIdAsync(int id);
        Task<Page<RouteDTO>> GetAllRouteByArrivalAndDepartureAndDateAsync(string ArrivalName,string DepartureName,DateOnly TimeToDeparture,int page, int size, string sort);
        Task<Page<RouteDTO>> GetAllRouteByArrivalName(string ArrivalName,int page,int size, string sort);
    }
}
