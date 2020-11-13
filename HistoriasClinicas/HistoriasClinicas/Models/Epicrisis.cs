using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas.Models
{
    public class Epicrisis
    {
        public int Id { get; set; }
        public Medico Medico { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaYHora { get; set; }
        public Diagnostico Diagnostico { get; set; }

        public int IdEpisodio { get; set; }
        public Episodio Episodio { get; set; }
    }
}
