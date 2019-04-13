using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TB.Web.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Campo requerido")]
        [Display(Name = "Usuario *")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo requerido")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña *")]
        public string Password { get; set; }
    }
}