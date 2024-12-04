using BookTour.Application.Dto;
using BookTour.Application.Dto.Request;
using BookTour.Application.Interface;
using BookTour.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Service
{
    public class LegService : ILegService
    {
        private readonly ILegRepository _legRepository;

        public LegService(ILegRepository legRepository)
        {
            _legRepository = legRepository;
        }

        public async Task<List<LegDTO>> GetAllLegByDetailRouteIdAsync(int detailRouteId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> InsertAsync(LegRequest l)
        {
            var leg = new Domain.Entity.Leg
            {
                Title = l.Title,
                Description = l.Description,
                Sequence = l.Sequence,
                DetailRouteId = l.detailRouteId
            };
            var result = await _legRepository.InsertAsync(leg);
            return result;
        }

        public async Task<bool> UpdateAsync(int legId, LegRequest l)
        {
            try
            {
                // Fetch the existing Leg
                var leg = await _legRepository.GetByIdAsync(legId);

                // Map updated values
                leg.Title = l.Title;
                leg.Description = l.Description;
                leg.Sequence = l.Sequence;
                leg.DetailRouteId = l.detailRouteId;    

                // Update in repository
                var result = await _legRepository.UpdateAsync(leg);
                return result;  
            }
            catch (Exception ex)
            {
                // Log error
                Console.WriteLine($"Error updating leg: {ex.Message}");
                return false;
            }
        }
    }
}
