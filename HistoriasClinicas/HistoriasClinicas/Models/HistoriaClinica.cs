using System.Collections.Generic;

namespace HistoriasClinicas.Models
{
    public class HistoriaClinica
    {
        public int Id { get; set; }
        public IEnumerable<Episodio> Episodios { get; set; }

        public int IdPaciente { get; set; }
        public Paciente Paciente { get; set; }
    }
}