using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HistoriasClinicas.Models
{
    public class Evolucion
    {
        public int Id { get; set; }
        public Medico Medico { get; set; }
        public string DescripcionAtencion { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaYHora { get; set; }
        public bool EstadoAbierto { get; set; }
        public List<Nota> Notas { get; set; }
    }
}
