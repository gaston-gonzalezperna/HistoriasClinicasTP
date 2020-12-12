using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas2.Models
{
    public class Epicrisis
    {
        [Key]
        public int Id { get; set; }
        public string NombreMedico { get; set; }
        public DateTime FechaYHora { get; set; }
        public Diagnostico Diagnostico { get; set; }
        public int EpisodioId { get; set; }

        [ForeignKey("EpisodioId")]
        public virtual Episodio Episodio { get; set; }
    }
}
