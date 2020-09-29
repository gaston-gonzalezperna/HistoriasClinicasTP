using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas.Models
{
    public class Epicrisis
    {
        public Episodio episodio { get; set; }
        public Medico medico { get; set; }
        public DateTime fechaYHora { get; set; }
        public Diagnostico diagnostico { get; set; }
    }
}
