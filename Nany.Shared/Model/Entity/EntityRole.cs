
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nany.Shared
{    
    public class EntityRole : EntityBase
    {
        public string RoleName { get; set; } = string.Empty;

        public string? ReportToRoleId { get; set; } = string.Empty;

        public string RoleDesc { get; set; } = string.Empty;

        public List<EntityRoleFunctionPermission> RolePermissions { get; set; } = new List<EntityRoleFunctionPermission>();

         
    }
}
