using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas2.Models
{
    public class Medico : Persona
    {
        public string Matricula { get; set; }
        public Especialidad Especialidad { get; set; }
    }
}
