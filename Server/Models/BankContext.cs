using Microsoft.EntityFrameworkCore;

namespace Server.Models
{
    public class BankContext : DbContext 
    {
        public DbSet<Banka> Banke { get; set; } // referenca na podatke koji je nas contet pokupio iz baze podataka, u ovom slucaju su to Banke

        public DbSet<Korisnik> Korisnici { get; set; }

        public DbSet<Kredit> Krediti { get; set; }

        public BankContext(DbContextOptions options) : base( options )
        {

        } 
    } 
}