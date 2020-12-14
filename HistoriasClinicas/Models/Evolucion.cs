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

        [Display(Name = "Descripcion")]
        public string DescripcionAtencion { get; set; }

        [Display(Name = "Inicio Evolucion")]
        public DateTime FechaYHora { get; set; }

        [Display(Name = "Alta Evolucion")]
        public DateTime FechaYHoraAlta { get; set; }

        [Display(Name = "Cierre Evolucion")]
        public DateTime FechaYHoraCierre { get; set; }

        [Display(Name = "Estado")]
        public bool EstadoAbierto { get; set; }
        public List<Nota> Notas { get; set; }
        public int EpisodioId { get; set; }
        [ForeignKey("EpisodioId")]
        public virtual Episodio Episodio { get; set; }
    }
}
