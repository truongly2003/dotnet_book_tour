using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Dto.Request
{
    public class PermissionOperationsRequest
    {
       public int permissionId { get; set; }
       public List<int> operationIds { get; set; }
    }
}
