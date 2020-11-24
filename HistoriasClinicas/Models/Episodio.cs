using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas2.Models
{
    public class Episodio
    {
        public int Id { get; set; }
        public string Motivo { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaYHoraInicio { get; set; }
        public DateTime FechaYHoraAlta { get; set; }
        public DateTime FechaYHoraCierre { get; set; }
        public List<Evolucion> Evoluciones { get; set; }
        public bool EstadoAbierto { get; set; }
        public Epicrisis Epicrisis { get; set; }
        public string EmpleadoRegistra { get; set; }
        public int HistoriaClinicaId { get; set; }

        [ForeignKey("HistoriaClinicaId")]
        public virtual HistoriaClinica HistoriaClinica { get; set; }

    }
}
