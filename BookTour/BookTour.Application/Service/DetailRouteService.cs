using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookTour.Application.Dto.Request;
using BookTour.Domain.Entity;
using BookTour.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using BookTour.Application.Dto;
using BookTour.Application.Interface;

namespace BookTour.Application.Service
{
    public class DetailRouteService : IDetailRouteService
    {
        private readonly IDetailRouteRepository _detailRouteRepository;
        private readonly IRouteRepository _routeRepository;
        private readonly IBookingRepository _bookingRepository;

        public DetailRouteService(
            IDetailRouteRepository detailRouteRepository,
            IRouteRepository routeRepository,
            IBookingRepository bookingRepository)
        {
            _detailRouteRepository = detailRouteRepository;
            _routeRepository = routeRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<Page<DetailRouteResponse>> GetAllDetailRouteAsync(int page, int size)
        {
            // Fetch all detail routes from the repository
            var data = await _detailRouteRepository.GetAllDetailRouteAsync();

            // Apply paging logic
            var paginatedData = data.Skip((page - 1) * size).Take(size).ToList();

            // Map to response DTO
            var detailRouteResponse = paginatedData.Select(detailRoute => new DetailRouteResponse
            {
                DetailRouteId = detailRoute.DetailRouteId,
                RouteId = detailRoute.RouteId, // Handle null Route
                DetailRouteName = detailRoute.DetailRouteName,
                DepartureName = detailRoute.Route?.Arrival?.ArrivalName ?? "Unknown",
                Description = detailRoute.Description,
                Stock = detailRoute.Stock,
                Rating = detailRoute.Feedbacks.Any() ? detailRoute.Feedbacks.Average(f => f.Rating) : 0,
                Price = detailRoute.Price,
                TimeToDeparture = detailRoute.TimeToDeparture,
                TimeToFinish = detailRoute.TimeToFinish,

                // Map ImageList
                ImageList = detailRoute.Images?.Select(image => new ImageDTO
                {
                    Id = image.ImageId,
                    TextImage = image.TextImage,
                }).ToList(),

                // Map LegList
                LegList = detailRoute.Legs?.Select(leg => new LegDTO
                {
                    Id = leg.LegId,
                    Title = leg.Title,
                    Description = leg.Description,
                    Sequence = leg.Sequence
                }).ToList()
            }).ToList();
            var totalElement = data.Count();
            var totalPage = (int)Math.Ceiling((double)totalElement / size);
            var result = new Page<DetailRouteResponse>
            {
                Data = detailRouteResponse,
                TotalElement = totalElement,
                TotalPages = totalPage
            };
            return result;

        }

        public async Task<DetailRouteResponse> GetDetailRouteByIdAsync(int id)
        {
            try
            {
                // Fetch the DetailRoute by ID from the repository
                var detailRoute = await _detailRouteRepository.GetDetailRouteByIdAsync(id);

                if (detailRoute == null)
                {
                    return null; // Return null if the DetailRoute doesn't exist
                }

                // Map to response DTO
                var response = new DetailRouteResponse
                {
                    DetailRouteId = detailRoute.DetailRouteId,
                    RouteId = detailRoute.RouteId,
                    DetailRouteName = detailRoute.DetailRouteName,
                    DepartureName = detailRoute.Route?.Arrival?.ArrivalName ?? "Unknown",
                    Description = detailRoute.Description,
                    Stock = detailRoute.Stock,
                    Rating = detailRoute.Feedbacks?.Any() == true
                        ? detailRoute.Feedbacks.Average(f => f.Rating) // Calculate average rating if feedback exists
                        : 0f,
                    Price = detailRoute.Price,
                    TimeToDeparture = detailRoute.TimeToDeparture,
                    TimeToFinish = detailRoute.TimeToFinish,

                    // Map ImageList
                    ImageList = detailRoute.Images?.Select(image => new ImageDTO
                    {
                        Id = image.ImageId,
                        TextImage = image.TextImage,
                    }).ToList(),

                    // Map LegList
                    LegList = detailRoute.Legs?.Select(leg => new LegDTO
                    {
                        Id = leg.LegId,
                        Title = leg.Title,
                        Description = leg.Description,
                        Sequence = leg.Sequence
                    }).ToList()
                };

                return response;
            }
            catch (Exception ex)
            {
                // Handle error, log if necessary
                Console.WriteLine($"Error fetching detail route by ID: {ex.Message}");
                return null; // Return null in case of an error
            }
        }

        private List<Image> MapImages(List<ImageRequest> imageRequests, Detailroute detail)
        {
            return imageRequests.Select(imageRequest => new Image
            {
                TextImage = imageRequest.TextImage,
                IsPrimary = imageRequest.isPrimary,
                DetailRoute = detail
            }).ToList();
        }

        private List<Leg> MapLegs(List<LegRequest> legRequests, Detailroute detail)
        {
            return legRequests.Select(legRequest => new Leg
            {
                Title = legRequest.Title,
                Description = legRequest.Description,
                Sequence = legRequest.Sequence,
                DetailRoute = detail
            }).ToList();
        }

        public async Task<bool> InsertAsync(DetailRouteRequest detailRouteRequest)
        {
            var detail = new Detailroute
            {
                DetailRouteName = detailRouteRequest.DetailRouteName,
                Price = detailRouteRequest.Price,
                Stock = detailRouteRequest.Stock,
                TimeToDeparture = detailRouteRequest.TimeToDeparture,
                TimeToFinish = detailRouteRequest.TimeToFinish,
                Description = detailRouteRequest.Description
            };

            var route = await _routeRepository.GetByIdAsync(detailRouteRequest.RouteId);
            if (route == null)
            {
                throw new ArgumentException($"Route không tồn tại với ID: {detailRouteRequest.RouteId}");
            }

            detail.Route = route;

            if (detailRouteRequest.ImageList != null)
            {
                detail.Images = MapImages(detailRouteRequest.ImageList, detail);
            }

            if (detailRouteRequest.LegList != null)
            {
                detail.Legs = MapLegs(detailRouteRequest.LegList, detail);
            }

            await _detailRouteRepository.InsertAsync(detail);
            return true;
        }

        public async Task<bool> UpdateAsync(int detailRouteId, DetailRouteRequest detailRouteRequest)
        {
            var detail = await _detailRouteRepository.findById(detailRouteId);
            if (detail == null)
            {
                throw new ArgumentException($"DetailRoute không tồn tại với ID: {detailRouteId}");
            }

            // Cập nhật các thuộc tính cơ bản
            detail.DetailRouteName = detailRouteRequest.DetailRouteName;
            detail.Price = detailRouteRequest.Price;
            detail.Stock = detailRouteRequest.Stock;
            detail.TimeToDeparture = detailRouteRequest.TimeToDeparture;
            detail.TimeToFinish = detailRouteRequest.TimeToFinish;
            detail.Description = detailRouteRequest.Description;

            // Cập nhật Route nếu có RouteId
            if (detailRouteRequest.RouteId != 0)
            {
                var route = await _routeRepository.GetByIdAsync(detailRouteRequest.RouteId);
                if (route == null)
                {
                    throw new ArgumentException($"Route không tồn tại với ID: {detailRouteRequest.RouteId}");
                }
                detail.RouteId = route.RouteId;
            }

            // Cập nhật Images nếu có ImageList
            if (detailRouteRequest.ImageList != null && detailRouteRequest.ImageList.Any())
            {
                detail.Images = MapImages(detailRouteRequest.ImageList, detail);
            }

            // Cập nhật Legs nếu có LegList
            if (detailRouteRequest.LegList != null && detailRouteRequest.LegList.Any())
            {
                detail.Legs = MapLegs(detailRouteRequest.LegList, detail);
            }

            // Cập nhật vào DB
            await _detailRouteRepository.UpdateAsync(detail);
            return true;
        }


        public async Task<bool> DeleteAsync(int detailRouteId)
        {
            // Kiểm tra xem có đơn đặt nào liên quan đến DetailRoute này không
            var bookingExists = await _bookingRepository.ExistsByDetailRouteIdAsync(detailRouteId);

            if (bookingExists)
            {
                // Nếu có người đã đặt, không cho phép xóa
                throw new InvalidOperationException($"Không thể xóa DetailRoute với ID {detailRouteId} vì đã có người đặt.");
            }

            // Tiến hành xóa nếu không có đơn đặt
            var detailRoute = await _detailRouteRepository.findById(detailRouteId);
            if (detailRoute == null)
            {
                throw new ArgumentException($"DetailRoute không tồn tại với ID {detailRouteId}");
            }

            // Tiến hành xóa
            await _detailRouteRepository.DeleteAsync(detailRouteId);
            return true;
        }


    }
}
