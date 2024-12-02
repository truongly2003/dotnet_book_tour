using BookTour.Domain.Entity;

namespace BookTour.Domain.Interface
{
    public interface IPassengerRepository
    {
        Task<Passenger> SaveAsync(Passenger passenger);
    }
}