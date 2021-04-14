using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfO.Models
{
    public class UserRoleType
    {
        public int Id { get; set; } // UseRoleID (Primary key)
        public string Name { get; set; }

        // Reverse navigation
        public virtual ICollection<UserRole> UserRoles { get; set; } // Users.FK_Users_UserRoles
    }
}
