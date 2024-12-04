using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Dto.Request
{
    public class DetailRouteRequest
    {
     
        public int RouteId { get; set; }
        public string DetailRouteName { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }        
        public double Price { get; set; }
        public DateOnly TimeToDeparture { get; set; }
        public DateOnly TimeToFinish { get; set; }
        public List<ImageRequest>? ImageList{ get; set; }
        public List<LegRequest>? LegList { get; set; }
    }
}
