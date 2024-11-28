using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Dto.Request
{
    public class ImageRequest
    {
        public int detailRouteId { get; set; }
        public string TextImage { get; set; }

        public int isPrimary { get; set; }
    }
}
