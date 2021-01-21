using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Server.Models;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BankController : ControllerBase
    {
        public BankContext Context { get; set; } // properti koji ce da ukazuje na context, tj preko njega preuzimamo podatke iz baze podataka
        public BankController(BankContext context)
        {
            Context = context ;
        }

        // pisemo sada metode controllera
        
        [Route("PreuzmiBanku")]
        [HttpGet]
        public async Task<List<Banka>> PreuzmiBanku() // radi okej
        {
            return await Context.Banke.Include(p => p.Krediti).Include(k => k.Korisnici).ToListAsync();
        }

        [Route("UnesiBanku")]
        [HttpPost]
        public async Task UnesiBanku([FromBody] Banka banka) // radi okej
        {
            Context.Banke.Add(banka);
            await Context.SaveChangesAsync();
        }

        [Route("PromeniBanku")]
        [HttpPut]
        public async Task PromeniBanku([FromBody] Banka banka) // radi okej
        {
            Context.Update<Banka>(banka); // po prenesem id-ju u badiju fje mi znamo o kojoj banci je rec
            await Context.SaveChangesAsync();
        }

        [Route("IzbrisiBanku/{id}")]
        [HttpDelete]
        public async Task IzbrisiBanku(int id ) // radi okej // po prenesenom id-ju u ruti mi znamo koju banku da izbrisemo
        {
            var banka = await Context.Banke.FindAsync(id);
            Context.Remove(banka);
            await Context.SaveChangesAsync();
        }

        [Route("UnesiKorisnika/{idBanke}")]
        [HttpPost]
        public async Task<IActionResult> UnesiKorisnika(int idBanke,[FromBody] Korisnik korisnik) // radi okej
        {
            // treba da se proveri da li pri unosu korisnik vec postoji u banci
            var banka = await Context.Banke.FindAsync(idBanke);
            if(Context.Korisnici.Any(k => k.Ime == korisnik.Ime && k.Prezime == korisnik.Prezime && k.DatumRodjenja == korisnik.DatumRodjenja))
            {
                return StatusCode(406); // znaci postoji isti korisnik u banci i ne mozemo ponovo da ga unesemo
            }
            else
            {   // ne postoji vec takav korisnik u banci i mozemo da ga unesemo u banku
                korisnik.Banka = banka ;
                Context.Korisnici.Add(korisnik);
                await Context.SaveChangesAsync();
                return Ok();
            }
        }
        
        [Route("UnesiKredit/{idBanke}/{imeKorisnika}")]
        [HttpPost]
        public async Task<IActionResult> UnesiKredit(int idBanke,string imeKorisnika, [FromBody] Kredit kredit) // radi!!!
        {
            // sada treba da se proveri da li u banci postoji korisnik sa datim imenom i ako postoji da se kredit upise u banku 
            // i da se upise da u kredit pokazivac na banku i korisnika

            var banka = await Context.Banke.FindAsync(idBanke); // nadjemo banku 
            var ind = 0;
            if( Context.Korisnici.Any(k => k.Ime == imeKorisnika))
            {
                ind = 1;
            }
            if(ind == 1) // ako jeste znaci u banci postoji korisnik sa datim imenom
            {
                var korisnik = Context.Korisnici.Where(k => k.Ime == imeKorisnika).First(); // sada smo nasli korisnika sa prenetim imenom
                kredit.Banka = banka; // kredit sada ukazuje na zeljenu banku
                kredit.Korisnik = korisnik; // kredit sada ukazuje na svog korisnika
                Context.Krediti.Add(kredit);
                await Context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return StatusCode(406);
            }
        }

        [Route("OtplatiKredit/{idBanke}/{brojKredita}/{iznosKredita}")]
        [HttpPut]
        public async Task<IActionResult> OtplatiKredit(int idBanke, int brojKredita, int iznosKredita)
        {
            var banka = await Context.Banke.FindAsync(idBanke);
            if( Context.Krediti.Any( k => k.Broj == brojKredita))
            { 
                var kred = Context.Krediti.Where( k => k.Broj == brojKredita).FirstOrDefault();
                if( iznosKredita + kred.VracenIznos > kred.Iznos)
                {
                    return StatusCode(406);
                }
                else {
                    kred.VracenIznos+= iznosKredita;
                    await Context.SaveChangesAsync();
                    return Ok();
                }
            }
            return StatusCode(406);
        }

        [Route("IzbrisiKredit/{idBanke}/{brojKredita}")]
        [HttpDelete]
        public async Task<IActionResult> IzbrisiKredit(int idBanke, int brojKredita)
        {
            var banka = await Context.Banke.FindAsync(idBanke);
            if( Context.Krediti.Any( k => k.Broj == brojKredita))
            { 
                var kred = Context.Krediti.Where( k => k.Broj == brojKredita).FirstOrDefault();
                if( kred.VracenIznos == kred.Iznos)
                {
                    Context.Krediti.Remove(kred);
                    await Context.SaveChangesAsync();
                    return Ok();
                }
            }
            return StatusCode(406);
        }

    }
}
