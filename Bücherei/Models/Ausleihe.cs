using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bücherei.Models
{
    [Table("TblAusleihe")]
    public class Ausleihe
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Buchnummer { get; set; }
        [ForeignKey("Buchnummer")]
        public virtual Buch Buch { get; set; }
        [Required]
        public int Ausweisnummer { get; set; }
        [ForeignKey("Ausweisnummer")]
        public virtual SchuelerIn SchuelerIn { get; set; }
        [Required]
        public DateTime Ausleihdatum { get; set; }
        public DateTime? Retourdatum { get; set; }


       // public virtual List<Buch> LeihBuecher { get; set; }
    }
}
