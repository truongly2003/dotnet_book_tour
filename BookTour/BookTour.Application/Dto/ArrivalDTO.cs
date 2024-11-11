using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Dto
{
    public class ArrivalDTO
    {
        public int Id { get; set; }
        public string ArrivalName { get; set; }
        public string TextImage { get; set; }

        public int CountRoute { get; set; }
    }
}
