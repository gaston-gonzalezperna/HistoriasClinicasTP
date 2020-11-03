using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas.Models
{
    public class Diagnostico
    {
        public int ID { get; set; }
        public Epicrisis Epicrisis { get; set; }
        public string Descripcion { get; set; }
        public string Recomendacion { get; set; }
    }
}
