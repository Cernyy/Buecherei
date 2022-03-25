using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bücherei.Models
{
    [Table("TblBuch")]
    public class Buch
    {
        [MaxLength(6), Key]
        public string Buchnummer { get; set; }
        [MaxLength(25), Required]
        public string Sachgebiet { get; set; }
        [MaxLength(50), Required]
        public string Titel { get; set; }
        [MaxLength(50), Required]
        public string Autor { get; set; }
        [MaxLength(50), Required]
        public string Ort { get; set; }
        [Required]
        public int Erscheinungsjahr { get; set; }
        //public virtual List<Ausleihe> Ausleihen { get; set; }
    }
}
