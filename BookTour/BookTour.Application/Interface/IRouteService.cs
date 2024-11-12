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
        Task<Page<RouteDTO>> GetAllRouteByArrivalAndDepartureAndDateAsync(string arrivalName,string departureName,DateOnly timeToDeparture,int page, int size, string sort);
        Task<Page<RouteDTO>> GetAllRouteByArrival(string arrivalName,int page,int size, string sort);


        Task<Page<RouteDTO>> getAllRouteAsyncTestPage(int page, int size, string sort);

        Task<List<Route>> getAllRoute();
    }
}
