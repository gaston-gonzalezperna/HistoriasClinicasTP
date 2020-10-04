using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas.Models
{
    public class Medico : Persona
    {
        public string Matricula { get; set; }
        public string Especialidad { get; set; }
    }
}
