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
    public class ArrivalService : IArrivalService
    {
        private readonly IArrivalRepository _arrivalRepository;
        public ArrivalService(IArrivalRepository arrivalRepository)
        {
            _arrivalRepository = arrivalRepository;
        }

        public async Task<List<ArrivalDTO>> getAllArrivalAsync()
        {
            var data=await _arrivalRepository.getAllArrivalAsync();
           
            var arrivalDTO = data.Select(arrival => new ArrivalDTO
            {
                Id = arrival.ArrivalId,
                ArrivalName = arrival.ArrivalName,
                CountRoute=arrival.Routes.Sum(route=>route.Detailroutes.Count),
            }).ToList();
            return arrivalDTO;
        }
    }
}
