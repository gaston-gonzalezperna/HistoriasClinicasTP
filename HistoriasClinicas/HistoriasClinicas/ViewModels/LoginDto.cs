using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas.ViewModels
{
    public class LoginDto
    {
        [Required]
        [EmailAddress(ErrorMessage = "Ingresá un correo amigo")]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
