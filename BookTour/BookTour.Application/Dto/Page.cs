using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Dto
{
    public class Page<T>
    {
        public long TotalElement { get; set; }
        public int TotalPages { get; set; }
        public List<T> Data { get; set; } = new List<T>();

    }
}
