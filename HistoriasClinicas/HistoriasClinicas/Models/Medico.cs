using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas.Models
{
    public class Medico : Persona
    {
        public int matricula { get; set; }
        public String especialidad { get; set; }
    }
}
