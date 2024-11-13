using BookTour.Application.Dto;
using BookTour.Application.Interface;
using BookTour.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Service
{
    public class DepartureService : IDepartureService
    {
        private readonly IDepartureRepository _departureRepository;
        public DepartureService(IDepartureRepository departureRepository)
        {
            _departureRepository = departureRepository;
        }
        public async Task<List<DepartureDTO>> getAllDepartureAsync()
        {
            var data = await _departureRepository.getAllDepartureAsync();
            var departureDTO = data.Select(departure => new DepartureDTO
            {
                Id = departure.DepartureId,
                DepartureName = departure.DepartureName
            }).ToList();
            return departureDTO;
        }
    }
}
