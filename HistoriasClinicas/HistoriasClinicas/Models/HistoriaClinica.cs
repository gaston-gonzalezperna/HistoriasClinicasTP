using System.Collections.Generic;

namespace HistoriasClinicas.Models
{
    public class HistoriaClinica
    {
        public Paciente Paciente { get; set; }
        public List<Episodio> Episodios { get; set; }
    }
}