import { Banka } from "./banka.js";
import { Kredit } from "./kredit.js";
import { Korisnik } from "./korisnik.js";

// var banka1 = new Banka(0,"Centralna Banka", 1000000, 50);
// banka1.crtaj(document.body);

fetch("https://localhost:5001/Bank/PreuzmiBanku").then(p => {
    p.json().then(data => {
        data.forEach(banka => {
            const banka1 = new Banka(banka.id,banka.naziv,banka.vrednost,banka.kapacitet);
            banka1.crtaj(document.body);

            
            const mesto = banka1.container.querySelector(".platforma").querySelector(".nosacKredita");
            //console.log(mesto);
            //console.log(banka);
            //console.log(banka1);
            
            banka.krediti.forEach((kred) => {
                //console.log(kred);
                banka1.dodajKredit(new Kredit(kred.broj,kred.iznos,kred.datumPodizanja,kred.datumIsplate,kred.vracenIznos,new Korisnik(kred.korisnik.ime, kred.korisnik.prezime, kred.korisnik.datumRodjenja)));
            })
            banka.korisnici.forEach((kor) => {
                banka1.dodajKorisnika(new Korisnik(kor.ime,kor.prezime,kor.datumRodjenja));
            })
            banka1.krediti.forEach(k => {
                k.crtajKredit(mesto); // ne znam zasto ovo nece kaze da ovo nije fja!!!
            })
        });
    });
});