using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas.Models
{
    public class Diagnostico
    {
        public Epicrisis epicrisis { get; set; }
        public String descripcion { get; set; }
        public String recomendacion { get; set; }
    }
}
