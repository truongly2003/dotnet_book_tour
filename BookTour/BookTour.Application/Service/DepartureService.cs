using BookTour.Application.Dto;
using BookTour.Application.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Service
{
    public class DepartureService : IDepartureService
    {
        public async Task<List<DepartureDTO>> getAllDepartureAsync()
        {
            throw new NotImplementedException();
        }
    }
}
