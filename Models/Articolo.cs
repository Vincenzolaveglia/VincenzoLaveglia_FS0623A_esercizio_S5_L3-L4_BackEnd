namespace Scarpe___Co.Models
{
    public class Articolo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public double Prezzo { get; set; }
        public string Descrizione { get; set; }
        public string ImgCopertina { get; set; }
        public string ImgAggiuntive { get; set; }
        public DateTime? DeletedAt { get; set; } = null;
    }
}
