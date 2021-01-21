using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    [Table("Banka")] // kako ce da nam se zove tabela u bazi podataka
    public class Banka
    {   
        [Key]
        [Column("ID")]
        public int ID { get; set; }

        [Column("Naziv")]
        [MaxLength(255)]
        public string Naziv { get; set; }

        [Column("Vrednost")]
        public int Vrednost { get; set; }

        [Column("Kapacitet")]
        public int Kapacitet { get; set; }

        public virtual List<Korisnik> Korisnici { get; set; } // pokazuje na korisnike koji se nalaze u banci

        public virtual List<Kredit> Krediti { get; set; } // pokazuje na kredite u banci u bazi podataka
    }
}