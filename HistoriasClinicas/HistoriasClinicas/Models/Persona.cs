using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace HistoriasClinicas.Models
{
    public abstract class Persona : IdentityUser<int>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        [RegularExpression(@"[0-9]{2}\.[0-9]{3}\.[0-9]{3}")]
        public string DNI { get; set; }
        public string Direccion { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaAlta { get; set; }
    }
}
