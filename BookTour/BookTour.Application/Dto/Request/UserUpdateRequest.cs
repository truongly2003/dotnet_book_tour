using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Dto.Request
{
    public class UserUpdateRequest
    {
        public int id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public int roleId { get; set; }
    }
}
