using BookTour.Application.Dto;
using BookTour.Application.Interface;
using BookTour.Domain.Entity;
using BookTour.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Service
{
    public class RouteService : IRouteService
    {
        
        private readonly IRouteRepository _routeRepository;
        public RouteService(IRouteRepository routeRepository)
        {
            _routeRepository = routeRepository;
        }
        // get all route
        public async Task<Page<RouteDTO>> GetAllRouteAsync(int page, int size, string sort)
        {
            var data = await _routeRepository.GetAllRouteAsync();
            if (sort == "asc")
            {
                data = data.OrderBy(d => d.Price).ToList();
            }
            else if (sort == "desc")
            {
                data = data.OrderByDescending(d => d.Price).ToList();
            }
            var routeDTO = data.Select(detail => new RouteDTO
            {
                DetailRouteId = detail.DetailRouteId,
                DetailRouteName = detail.DetailRouteName,
                Description = detail.Description,
                Price = detail.Price,
                TimeToDeparture = detail.TimeToDeparture,
                TimeToFinish = detail.TimeToFinish,
                Stock = detail.Stock,
                RouteId = detail.RouteId,
                ImageId = detail.Images.First().ImageId,
                TextImage = detail.Images.First().TextImage ?? string.Empty,
                Rating = detail.Feedbacks.Any() ? detail.Feedbacks.Average(f => f.Rating) : 0
            })
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();
            var totalElement = data.Count();
            var totalPage = (int)Math.Ceiling((double)totalElement / size);
            var result = new Page<RouteDTO>
            {
                Data = routeDTO,
                TotalElement = totalElement,
                TotalPages = totalPage
            };
            return result;
        }
        // get route by arrivalName
        public async Task<Page<RouteDTO>> GetAllRouteByArrivalName(string ArrivalName, int page, int size, string sort)
        {
            var data = await _routeRepository.GetAllRouteByArrivalNameAsync(ArrivalName);
     
            if (sort == "asc")
            {
                data = data.OrderBy(d => d.Price).ToList();
            }
            else if (sort == "desc")
            {
                data = data.OrderByDescending(d => d.Price).ToList();
            }
            var routeDTO = data.Select(detail => new RouteDTO
            {
                DetailRouteId = detail.DetailRouteId,
                DetailRouteName = detail.DetailRouteName,
                Description = detail.Description,
                Price = detail.Price,
                TimeToDeparture = detail.TimeToDeparture,
                TimeToFinish = detail.TimeToFinish,
                Stock = detail.Stock,
                RouteId = detail.RouteId,
                ImageId = detail.Images.FirstOrDefault()?.ImageId ?? 0,
                TextImage = detail.Images.FirstOrDefault()?.TextImage ?? string.Empty,
                Rating = detail.Feedbacks.Any() ? detail.Feedbacks.Average(f => f.Rating) : 0
            })
            .Skip((page - 1) * size)
            .Take(size)
            .ToList();
            var totalElement = data.Count();
            var totalPage = (int)Math.Ceiling((double)totalElement / size);
            var result = new Page<RouteDTO>
            {
                Data = routeDTO,
                TotalElement = totalElement,
                TotalPages = totalPage
            };
            return result;
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
