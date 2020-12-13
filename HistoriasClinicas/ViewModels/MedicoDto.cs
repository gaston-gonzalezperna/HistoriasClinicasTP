using HistoriasClinicas2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas.ViewModels
{
    public class MedicoDto : BaseDto
    {
        private const String _campoReqMsg = "El campo {0} es requerido.";

        [Required(ErrorMessage = _campoReqMsg)]
        [MaxLength(6, ErrorMessage = "El campo {0} debe tener como máximo {1} caracteres.")]
        [MinLength(5, ErrorMessage = "El campo {0} debe tener como mínimo {1} caracteres.")]
        public string Matricula { get; set; }
        [Required]
        public Especialidad Especialidad { get; set; }
    }
}