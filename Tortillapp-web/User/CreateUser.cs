using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tortillapp_web.Model
{
    public partial class CreateUser
    {
        public ushort UserId { get; set; }
        public string UserMail { get; set; }
        public string UserName { get; set; } 
        public string UserPass { get; set; } 
        public byte RoleId { get; set; }
        public string? ShowName { get; set; }
        public byte[]? ShowPic { get; set; }
        /// <summary>
        /// 1 = No, 2 = Si
        /// </summary>
        public sbyte RemenberMe { get; set; }
        [DataType(DataType.Date)]
        public DateTime UserCreated { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
