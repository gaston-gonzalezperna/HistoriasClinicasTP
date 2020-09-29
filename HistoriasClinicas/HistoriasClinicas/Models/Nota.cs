using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas.Models
{
    public class Nota
    {
        public Evolucion evolucion { get; set; }
        public Empleado empleado { get; set; }
        public String mensaje { get; set; }
        public DateTime fechaYHora { get; set; }
    }
}
