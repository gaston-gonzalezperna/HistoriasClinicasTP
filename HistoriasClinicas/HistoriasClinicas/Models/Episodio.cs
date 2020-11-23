using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HistoriasClinicas.Models
{
    public class Episodio
    {
        public int Id { get; set; }
        public string Motivo { get; set; }
        public string Descripcion { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaYHoraInicio { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaYHoraAlta { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaYHoraCierre { get; set; }
        public List<Evolucion> Evoluciones { get; set; }
        public bool EstadoAbierto { get; set; }
        public Epicrisis Epicrisis { get; set; }
        public Empleado EmpleadoRegistra { get; set; }
        public int HistoriaClinicaId { get; set; }
    }
}