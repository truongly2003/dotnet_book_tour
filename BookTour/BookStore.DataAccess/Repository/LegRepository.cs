using BookTour.Domain.Entity;
using BookTour.Domain.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class LegRepository : ILegRepository
    {
        private readonly BookTourDbContext _dbContext;
        public LegRepository(BookTourDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Leg>> GetAllLegByDetailRouteIdAsync(int detailRouteId)
        {
            var query = await _dbContext.Legs
                .Where(leg => leg.DetailRouteId == detailRouteId)
                .ToListAsync();

            return query;
        }

        public async Task<Leg> GetByIdAsync(int id)
        {
            return await _dbContext.Legs.FirstOrDefaultAsync(d => d.LegId == id);
        }

        public async Task<bool> InsertAsync(Leg leg)
        {
            try
            {
                await _dbContext.Legs.AddAsync(leg);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inserting leg: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Leg leg)
        {
            try
            {
                var existingLeg = await _dbContext.Legs.FindAsync(leg.LegId);
                if (existingLeg == null) return false;

                existingLeg.Title = leg.Title;
                existingLeg.Description = leg.Description;
                existingLeg.Sequence = leg.Sequence;

                _dbContext.Legs.Update(existingLeg);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating leg: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int legId)
        {
            try
            {
                var leg = await _dbContext.Legs.FindAsync(legId);
                if (leg == null) return false;

                _dbContext.Legs.Remove(leg);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting leg: {ex.Message}");
                return false;
            }
        }
    }
}
