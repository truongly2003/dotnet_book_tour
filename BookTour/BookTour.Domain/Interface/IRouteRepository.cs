using BookTour.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Domain.Interface
{
    public interface IRouteRepository
    {
        Task<List<Detailroute>> GetAllRouteAsync();
        Task<List<Detailroute>> GetAllRouteByArrivalNameAsync(string ArrivalName);
        Task<List<Detailroute>> GetAllRouteByArrivalAndDepartureAndDateAsync(string ArrivalName, string DepartureName, DateOnly TimeToDeparture);
        Task<Detailroute> GetDetailRouteByIdAsync(int DetailRouteId);
        Task<Route> GetByIdAsync(int id);
        Task<List<Route>> GetRoutesWithDetailsAsync();

    }
}
