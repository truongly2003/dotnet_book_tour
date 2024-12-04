using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Dto
{
    public class UserDTO
    {
        public int id { get; set; }
        public string username {get; set; }
        public string password { get; set; }
        public string email { get; set; }

        public int roleId {  get; set; }
        public string roleName { get; set; }

        public int status { get; set; }

        public string token { get; set; }
    }
}
