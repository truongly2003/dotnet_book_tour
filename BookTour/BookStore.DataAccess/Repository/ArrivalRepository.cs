using BookTour.Domain.Entity;
using BookTour.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class ArrivalRepository : IArrivalRepository
    {
        public Task<List<Arrival>> getAllArrivalAsync()
        {
            throw new NotImplementedException();
        }
    }
}
