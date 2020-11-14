using HistoriasClinicas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas.ViewModels
{
    public class MedicoDto : BaseDto
    {
        public string Matricula { get; set; }
        public Especialidad Especialidad { get; set; }
    }
}
