using System.Collections.Generic;

namespace HistoriasClinicas.Models
{
    public class HistoriaClinica
    {
        public int ID { get; set; }
        public Paciente Paciente { get; set; }
        public List<Episodio> Episodios { get; set; }
    }
}