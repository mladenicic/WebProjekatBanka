using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Server.Models
{
    [Table("Korisnik")]
    public class Korisnik
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Ime")]
        [MaxLength(255)]
        public string Ime { get; set; }

        [Column("Prezime")]
        [MaxLength(255)]
        public string Prezime { get; set; }

        [Column("Datum Rodjenja")]
        [MaxLength(255)]
        public string DatumRodjenja { get; set; }

        [JsonIgnore]
        public virtual List<Kredit> Krediti { get; set; } // pokazujemo na listu kredita // *** mozda ce da pravi problem videcemo! ***

        [JsonIgnore] // necemo da serijalizujemo ovu banku nego samo da imamo pokazivac na nju
        public Banka Banka { get; set; } // pokazivac na banku jer se korisnici sadrze u njoj
    }
}