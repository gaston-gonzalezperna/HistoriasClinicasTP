using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HistoriasClinicas.ViewModels
{
    public class BaseDto
    {
        private const String _campoReqMsg = "El campo {0} es requerido.";

        public int Id { get; set; }
        [Required(ErrorMessage = _campoReqMsg)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = _campoReqMsg)]
        public string Apellido { get; set; }
        [Required(ErrorMessage = _campoReqMsg)]
        [RegularExpression(@"[0-9]{8}", ErrorMessage = "DNI mal ingresado")]
        public string DNI { get; set; }
        [Required(ErrorMessage = _campoReqMsg)]
        public string Direccion { get; set; }
        [Required(ErrorMessage = _campoReqMsg)]
        [Range(1000000, 999999999, ErrorMessage = "Numero invalido")]
        public string Telefono { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Ingresá un correo por favor")]
        public string Email { get; set; }
        [Required(ErrorMessage = _campoReqMsg)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}