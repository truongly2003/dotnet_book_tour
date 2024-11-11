using BookTour.Application.Dto;
using BookTour.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Service
{
    public class RouteService : IRouteService
    {
        public async Task<Page<RouteDTO>> GetAllRouteAsync(int page, int size, string sort)
        {
            throw new NotImplementedException();
        }

        public async Task<Page<RouteDTO>> GetAllRouteByArrival(string arrivalName, int page, int size, string sort)
        {
            throw new NotImplementedException();
        }

        public async Task<Page<RouteDTO>> GetAllRouteByArrivalAndDepartureAndDateAsync(string arrivalName, string departureName, DateOnly timeToDeparture, int page, int size, string sort)
        {
            throw new NotImplementedException();
        }

        public async Task<RouteDTO> GetDetailRouteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
