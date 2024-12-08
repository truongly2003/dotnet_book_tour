using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Dto.Request
{
    public class RolePermissionRequest
    {
        public int roleId { get; set; }
        public List<PermissionOperationsRequest> permissions { get; set; }
    }

}