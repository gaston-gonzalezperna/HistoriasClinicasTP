using System;
using System.ComponentModel.DataAnnotations;

namespace HistoriasClinicas.Models
{
    public abstract class Persona
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        [RegularExpression(@"[0-9]{2}\.[0-9]{3}\.[0-9]{3}")]
        public string DNI { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaAlta { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Usuario { get; set; }

        [DataType(DataType.Password,ErrorMessage = "Contraseña invalida")]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
    }
}
