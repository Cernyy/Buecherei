namespace BuechereiApp.Models
{
    public class Ausleihe { 
        public int Id { get; set; }
        public string Buchnummer { get; set; }
        public virtual Buch Buch { get; set; }
        public int Ausweisnummer { get; set; }
        public virtual SchuelerIn SchuelerIn { get; set; }
        public DateTime Ausleihdatum { get; set; }
        public DateTime? Retourdatum { get; set; }
    }
}
