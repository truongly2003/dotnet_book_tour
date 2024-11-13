﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Dto
{
    public class ApiResponse<T>
    {
        public int code { get; set; }
        public string message { get; set; }
        public T result { get; set; }

    }
}
