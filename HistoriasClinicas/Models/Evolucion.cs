using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas2.Models
{
    public class Evolucion
    {
        [Key]
        public int Id { get; set; }
        public string Medico { get; set; }
        public string DescripcionAtencion { get; set; }
        public DateTime FechaYHora { get; set; }
        public bool EstadoAbierto { get; set; }
        public List<Nota> Notas { get; set; }
        public int EpisodioId { get; set; }
        [ForeignKey("EpisodioId")]
        public virtual Episodio Episodio { get; set; }
    }
}
