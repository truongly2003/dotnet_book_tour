using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookTour.Application.Dto
{
    public class DecentralizationDTO
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public List<string> Permissions { get; set; }

        // Constructor mặc định
        public DecentralizationDTO()
        {
        }

        // Constructor với tham số
        public DecentralizationDTO(int roleId, string roleName, List<string> permissions)
        {
            RoleId = roleId;
            RoleName = roleName;
            Permissions = permissions;
        }

        // Builder pattern có thể được áp dụng trong C# với cách tạo các đối tượng
        public class Builder
        {
            private int _roleId;
            private string _roleName;
            private List<string> _permissions;

            public Builder SetRoleId(int roleId)
            {
                _roleId = roleId;
                return this;
            }

            public Builder SetRoleName(string roleName)
            {
                _roleName = roleName;
                return this;
            }

            public Builder SetPermissions(List<string> permissions)
            {
                _permissions = permissions;
                return this;
            }

            public DecentralizationDTO Build()
            {
                return new DecentralizationDTO(_roleId, _roleName, _permissions);
            }
        }
    }
}