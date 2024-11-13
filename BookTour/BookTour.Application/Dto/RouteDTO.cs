using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Dto
{
    public class RouteDTO
    {
        public int DetailRouteId { get; set; }
        public int RouteId { get; set; }
        public string DetailRouteName { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public DateOnly TimeToDeparture { get; set; }
        public DateOnly TimeToFinish { get; set; }
        public int ImageId { get; set; }
        public string TextImage { get; set; }
        public double Price { get; set; }
        public float Rating { get; set; }
        public int ArrivalId { get; set; }
        public string ArrivalName { get; set; }

    }
}
