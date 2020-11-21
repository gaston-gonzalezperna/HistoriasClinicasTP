using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas.Models
{
    public class Medico : Usuario
    {
        public string Matricula { get; set; }
        public Especialidad Especialidad { get; set; }
    }

}
