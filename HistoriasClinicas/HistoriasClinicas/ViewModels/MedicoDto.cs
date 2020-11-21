using HistoriasClinicas.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas.ViewModels
{
    public class MedicoDto : BaseDto
    {

        public string Matricula { get; set; }

        [EnumDataType(typeof(Especialidad))]
        public Especialidad Especialidad { get; set; }
    }
}
