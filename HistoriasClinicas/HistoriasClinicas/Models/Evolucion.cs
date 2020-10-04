using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HistoriasClinicas.Models
{
    public class Evolucion
    {
        public Medico Medico { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaYHoraInicio { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaYHoraAlta { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaYHoraCierre { get; set; }
        public string DescripcionAtencion { get; set; }
        public bool EstadoAbierto { get; set; }
        public List<Nota> Notas { get; set; }
    }
}
