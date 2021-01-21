export class Kredit{
    constructor(broj, iznos, datumPodizanja, datumIsplate, vracenIznos, korisnik){
        this.broj = broj;
        this.iznos = iznos;
        this.datumPodizanja = datumPodizanja;
        this.datumIsplate = datumIsplate;
        this.vracenIznos = vracenIznos;
        this.korisnik = korisnik;
        this.container = null;
    }
    crtajKredit(host){
        var element = document.createElement("div");
        this.container = element;
        element.className = "kredit";
        element.innerHTML = `Broj kredita:${this.broj}, Iznos:${this.iznos}, Datum podizanja:${this.datumPodizanja} , Datum isplate:${this.datumIsplate} , Vracen iznos:${this.vracenIznos}, Korisnik: ${this.korisnik.vratiPodatke()}`;
        host.appendChild(element);
    }
    otplati(suma){
        this.vracenIznos += suma;
        if(this.vracenIznos < this.iznos)
            this.container.innerHTML = `Broj kredita:${this.broj}, Iznos:${this.iznos}, Datum podizanja:${this.datumPodizanja} , Datum isplate:${this.datumIsplate} , Vracen iznos:${this.vracenIznos}, Korisnik: ${this.korisnik.vratiPodatke()}`;
        else{
            const roditelj = this.container.parentNode;
            roditelj.removeChild(this.container);
        }
    }
}