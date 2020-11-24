using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HistoriasClinicas.ViewModels
{
    public class PacienteDto : BaseDto
    {
        public string ObraSocial { get; set; }
    }
}