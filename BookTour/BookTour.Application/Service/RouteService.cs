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
        private readonly IImageRepository _imageRepository;
        private readonly ILegRepository _legRepository;
        public RouteService(IRouteRepository routeRepository, IImageRepository imageRepository, ILegRepository legRepository)
        {
            _routeRepository = routeRepository;
            _imageRepository = imageRepository;
            _legRepository = legRepository;
        }
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
                ImageId = detail.Images?.FirstOrDefault()?.ImageId ?? 0,
                TextImage = detail.Images?.FirstOrDefault()?.TextImage ?? string.Empty,
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
                ImageId = detail.Images?.FirstOrDefault()?.ImageId ?? 0,
                TextImage = detail.Images?.FirstOrDefault()?.TextImage ?? "default.jpg",

                Rating = detail.Feedbacks?.Any() == true ? detail.Feedbacks.Average(f => f.Rating) : 0
                
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

        public async Task<Page<RouteDTO>> GetAllRouteByArrivalAndDepartureAndDateAsync(string ArrivalName, string DepartureName, DateOnly TimeToDeparture, int page, int size, string sort)
        {
            var data = await _routeRepository.GetAllRouteByArrivalAndDepartureAndDateAsync(ArrivalName,DepartureName,TimeToDeparture);

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
                Rating = detail.Feedbacks?.Any() == true ? detail.Feedbacks.Average(f => f.Rating) : 0
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

        public async Task<DetailRouteResponse> GetDetailRouteByIdAsync(int DetailRouteId)
        {
            var detailRoute = await _routeRepository.GetDetailRouteByIdAsync(DetailRouteId);
            if (detailRoute == null)
            {
                throw new Exception("DetailRoute not found.");
            }
            var image = await _imageRepository.GetImageByDetailRouteIdAsync(DetailRouteId) ?? new List<Image>();
            var leg = await _legRepository.GetAllLegByDetailRouteIdAsync(DetailRouteId) ?? new List<Leg>();
            var detailDTO = new DetailRouteResponse
            {
                DetailRouteId = detailRoute.DetailRouteId,
                DetailRouteName = detailRoute.DetailRouteName,
                RouteId = detailRoute.RouteId,
                TimeToDeparture = detailRoute.TimeToDeparture,
                TimeToFinish = detailRoute.TimeToFinish,
                Description = detailRoute.Description,
                Price = detailRoute.Price,

                Rating = detailRoute.Feedbacks?.Any() == true ? detailRoute.Feedbacks.Average(f => f.Rating) : 0,
           
                Stock = detailRoute.Stock,
                DepartureName = detailRoute.Route.Departure.DepartureName ?? "Default Name",
                ImageList = image.Select(img => new ImageDTO
                {
                    Id = img.ImageId,
                    TextImage = img.TextImage ?? "default.jpg"
                }).ToList(),
                LegList = leg.Select(le => new LegDTO
                {
                    Id = le.LegId,
                    Title = le.Title,
                    Description = le.Description,
                    Sequence = le.Sequence
                }).ToList()
            };
            return detailDTO;
            throw new NotImplementedException();
        }
    }
}
