using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Server.Models
{
    [Table("Kredit")]
    public class Kredit
    {   
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Broj")]
        public int Broj { get; set; }

        [Column("Iznos")]
        public int Iznos { get; set; }

        [Column("Datum Podizanja")]
        [MaxLength(255)]
        public string DatumPodizanja { get; set; }

        [Column("Datum Isplate")]
        [MaxLength(255)]
        public string DatumIsplate { get; set; }

        [Column("Vracen Iznos")]
        public int VracenIznos { get; set; }

        [JsonIgnore] 
        public Banka Banka { get; set; } // pokazivac na banku jer se krediti sadrze u banci


        // [JsonIgnore] // znaci necu da serijalizujem korisnika nego samo da imam pokazivac na njega
        public Korisnik Korisnik { get; set; } // samo pokazivac na korisnika jer se krediti sadrze u njemu // ***Mozda ce da pravi problem videcemo!*** 
    }
}