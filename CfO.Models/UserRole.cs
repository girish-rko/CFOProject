using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfO.Models
{
    public class UserRole
    {
        public int Id { get; set; } // UserRoleID (Primary key)

        public int UserRoleTypeId { get; set; } //
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Comments { get; set; } // Comments (length: 255)


        public virtual UserRoleType UserRoleType { get; set; }
        // Reverse navigation
        public virtual ICollection<User> Users { get; set; } // Users.FK_Users_UserRoles

    }
}
