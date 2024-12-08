using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Dto
{
    public class RoleOperationResponse
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public List<PermissionDTO> Permissions { get; set; }

        // Default constructor
        public RoleOperationResponse() { }

        // Constructor with parameters
        public RoleOperationResponse(int roleId, string roleName, List<PermissionDTO> permissions)
        {
            RoleId = roleId;
            RoleName = roleName;
            Permissions = permissions;
        }
    }
}
