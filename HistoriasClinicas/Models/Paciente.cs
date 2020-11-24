using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas2.Models
{
    public class Paciente : Persona
    {
        public string ObraSocial { get; set; }
        public HistoriaClinica HistoriaClinica { get; set; }

    }
}
