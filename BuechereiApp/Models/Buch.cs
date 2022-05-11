namespace BuechereiApp.Models
{
    public class Buch
    {
        public string Buchnummer { get; set; }
        public string Sachgebiet { get; set; }
        public string Titel { get; set; }
        public string Autor { get; set; }
        public string Ort { get; set; }
        public int Erscheinungsjahr { get; set; }
    }
}
