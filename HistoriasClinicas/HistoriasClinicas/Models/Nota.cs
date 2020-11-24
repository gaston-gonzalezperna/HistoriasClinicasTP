using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas.Models
{
    public class Nota
    {
        public int ID { get; set; }
        public Evolucion Evolucion { get; set; }
        public Empleado Empleado { get; set; }
        public string Mensaje { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaYHora { get; set; }
    }
}
