using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas2.Models
{
    public class Nota
    {
        [Key]
        public int Id { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaYHora { get; set; }
        public string NombreAutor { get; set; }
        public int EvolucionId { get; set; }
        [ForeignKey("EvolucionId")]
        public virtual Evolucion Evolucion { get; set; }
    }
}
