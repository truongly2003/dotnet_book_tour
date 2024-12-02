using BookTour.Domain.Entity;
using BookTour.Domain.Interface;

namespace BookStore.DataAccess.Repository
{
    public class PassengerRepository : IPassengerRepository
    {
        private readonly BookTourDbContext _context;

        public PassengerRepository(BookTourDbContext context)
        {
            _context = context;
        }

        public async Task<Passenger> SaveAsync(Passenger passenger)
        {
            await _context.Passengers.AddAsync(passenger);
            await _context.SaveChangesAsync();

            return passenger;
        }
    }

}