using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HistoriasClinicas.ViewModels
{
    public class PacienteDto : BaseDto
    {
        private const String _campoReqMsg = "El campo {0} es requerido.";

        [Required(ErrorMessage = _campoReqMsg)]
        public string ObraSocial { get; set; }
    }
}