using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tortillapp_web._Model
{

    public partial class UserData
    {
        public int user_id { get; set; }
        public string? user_mail { get; set; }
        public string? user_name { get; set; }
        public string? user_pass { get; set; }
        //public virtual UserRoles urole { get; set; }
        public virtual UserRoles UserRole { get; set; }
        public string? show_name { get; set; }
        public string? show_pic { get; set; }
        public bool remember_me { get; set; }
        //[DataType(DataType.Date)]
        public DateTime user_created { get; set; }
        public DateTime last_updated {get; set; }
    }

    public partial class UserRoles
    {
        public int role_id { get; set; }
        public string? role_name { get; set; }
        public string? role_created { get; set; }
        public DateTime last_updated { get; set; }
    }

}
