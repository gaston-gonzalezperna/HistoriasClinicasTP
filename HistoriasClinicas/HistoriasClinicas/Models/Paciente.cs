namespace HistoriasClinicas.Models
{
    public class Paciente : Usuario
    {
        public string ObraSocial { get; set; }
        public HistoriaClinica HistoriaClinica { get; set; }
    }
}
