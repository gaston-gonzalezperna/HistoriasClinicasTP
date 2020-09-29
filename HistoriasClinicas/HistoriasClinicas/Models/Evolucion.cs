using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas.Models
{
    public class Evolucion
    {
        public Medico medico { get; set; }
        public DateTime fechaYHoraInicio { get; set; }
        public DateTime fechaYHoraAlta { get; set; }

        public DateTime fechaYHoraCierre { get; set; }
        public String descripcionAtencion { get; set; }
        public Boolean estadoAbierto { get; set; }
        public Nota notas { get; set; }
    }
}
