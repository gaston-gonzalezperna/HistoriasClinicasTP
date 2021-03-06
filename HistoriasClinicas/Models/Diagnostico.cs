using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas2.Models
{
    public class Diagnostico
    {
        [Key]
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Recomendacion { get; set; }

        public int EpicrisisId { get; set; }

        [ForeignKey("EpicrisisId")]
        public virtual Epicrisis Epicrisis { get; set; }

    }
}
