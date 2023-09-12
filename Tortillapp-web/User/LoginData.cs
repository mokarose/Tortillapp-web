using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tortillapp_web.Model
{
    public partial class LoginData
    {
        [Required]
        [Display(Name = "Correo")]
        public string iUserMail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string iPassword { get; set; }

    }
}
