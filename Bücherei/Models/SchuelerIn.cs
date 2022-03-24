using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bücherei.Models
{
    [Table("TblSchuelerIn")]
    public class SchuelerIn
    {
        [Key]
        public int Ausweisnummer { get; set; }
        [MaxLength(50), Required]
        public string Vorname { get; set; }
        [MaxLength(50), Required]
        public string Nachname { get; set; }
        public List<Ausleihe> Ausleihen { get; set; }
    }
}
