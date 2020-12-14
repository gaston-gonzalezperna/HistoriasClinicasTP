using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HistoriasClinicas2.Models
{
    public class Episodio
    {
        [Key]
        public int Id { get; set; }
        public string Motivo { get; set; }
        public string Descripcion { get; set; }

        [Display(Name = "Inicio de Episodio")]
        public DateTime FechaYHoraInicio { get; set; }

        [Display(Name = "Alta de Episodio")]
        public DateTime FechaYHoraAlta { get; set; }

        [Display(Name = "Cierre de Episodio")]
        public DateTime FechaYHoraCierre { get; set; }
        public List<Evolucion> Evoluciones { get; set; }

        [Display(Name = "Estado")]
        public bool EstadoAbierto { get; set; }
        public Epicrisis Epicrisis { get; set; }

        [Display(Name = "Registrado por..")]
        public string EmpleadoRegistra { get; set; }
        public int HistoriaClinicaId { get; set; }

        [ForeignKey("HistoriaClinicaId")]
        public virtual HistoriaClinica HistoriaClinica { get; set; }

    }
}
