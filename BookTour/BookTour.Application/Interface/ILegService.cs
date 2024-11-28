using BookTour.Application.Dto;
using BookTour.Application.Dto.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Interface
{
    public interface ILegService
    {
        Task<List<LegDTO>> GetAllLegByDetailRouteIdAsync(int detailRouteId);
        Task<bool> InsertAsync(LegRequest l);
        Task<bool> UpdateAsync(int id, LegRequest l);
    }
}
