using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas2.Models
{
    public class HistoriaClinica
    {
        [Key]
        public int Id { get; set; }
        public List<Episodio> Episodios { get; set; }
        public int PacienteId { get; set; }
        [ForeignKey("PacienteId")]
        public virtual Paciente Paciente { get; set; }
    }
}
