using System;
using System.Collections.Generic;

namespace Tortillapp_web.tortilla
{
    public partial class UserRole
    {
        public UserRole()
        {
            UserData = new HashSet<UserData>();
        }

        public byte RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public DateTime RoleCreated { get; set; }
        public DateTime LastUpdated { get; set; }

        public virtual ICollection<UserData> UserData { get; set; }
    }
}
