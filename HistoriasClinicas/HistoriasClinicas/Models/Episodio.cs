﻿using System;

namespace HistoriasClinicas.Models
{
    public class Episodio
    {
        public String motivo { get; set; }
        public String descripcion { get; set; }
        public DateTime fechaYHoraInicio { get; set; }
        public DateTime fechaYHoraAlta { get; set; }

        public DateTime fechaYHoraCierre { get; set; }
        public Evolucion evoluciones { get; set; }
        public Boolean estadoAbierto { get; set; }
        public Epicrisis epicrisis { get; set; }
        public Empleado empleadoRegistra { get; set; }
    }
}