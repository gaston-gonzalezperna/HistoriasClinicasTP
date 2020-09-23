namespace HistoriasClinicas.Models
{
    public class Paciente : Persona
    {
        public string ObraSocial { get; set; }
        public HistoriaClinica HistoriaClinica { get; set; }
    }
}
