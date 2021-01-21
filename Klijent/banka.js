import { Korisnik } from "./korisnik.js";
import { Kredit } from "./kredit.js";

export class Banka {
    constructor(id, naziv, vrednost, kapacitetKorisnika){ // dodat je id kao properti radi lakseg pisanja u bazi podataka!
        this.id = id;
        this.naziv = naziv;
        this.vrednost = vrednost; // properti za vrednost banke i kapacitet je cisto opisni.
        this.kapacitet = kapacitetKorisnika;
        this.korisnici = [];
        this.krediti = [];
        this.container = null;
    }
    dodajKorisnika(korisnik){
        this.korisnici.push(korisnik);
    }
    dodajKredit(kredit){
        this.krediti.push(kredit);
    }
    crtaj(host){
        if(!host)
            throw new Error("Unet je neispravan host!");
        
        var osnova = document.createElement("div");
        host.appendChild(osnova);
        this.container = osnova; // po osnoci crtamo banku
        osnova.className = "osnova";

        var formaUnosa = document.createElement("div");
        osnova.appendChild(formaUnosa);
        formaUnosa.className = "formaUnosa";

        this.crtajUnosKorisnika(formaUnosa);
        this.crtajUnosKredita(formaUnosa);
        this.crtajOtplatuKredita(formaUnosa);

        var platforma = document.createElement("div");
        platforma.className = "platforma";
        osnova.appendChild(platforma);

        this.crtajZaglavlje(platforma);

        var nosacKredita = document.createElement("div");
        nosacKredita.className = "nosacKredita";
        platforma.appendChild(nosacKredita);

        // ovo dole da iscrta kredite ako vec postoje u banci mada to nece koristimo valjda :D
        //*** Mozda mi ovo i ne treba :D !!! ***
        console.log(this.krediti);
        this.krediti.forEach((el) => {
            el.crtajKredit(nosacKredita); // ****!!!!! ovo nece da mi iscrta sve kredite koje imam u banci zato sto izgleda ti krediti nemaju pokazivace na korisnika na tamo u metodi .crtajKredit za innherHTML ne moze da se pozove na korisnika il sta vec!!!!!****
            console.log(nosacKredita);
            console.log(el);
        });
        
    }
    crtajZaglavlje(platforma){ //zavrsena
        var zaglavlje = document.createElement("label");
        zaglavlje.innerHTML = this.naziv;
        zaglavlje.className = "zaglavlje";
        platforma.appendChild(zaglavlje);
    }
    crtajUnosKorisnika(formaUnosa){ //zavrsena
        const unosKorisnika = document.createElement("div");
        formaUnosa.appendChild(unosKorisnika);
        unosKorisnika.className = "unosKorisnika";
        
        var labela = document.createElement("h3");
        labela.innerHTML = "Unos korisnika";
        unosKorisnika.appendChild(labela);

        labela = document.createElement("label");
        labela.innerHTML = "Ime :";
        unosKorisnika.appendChild(labela);

        let ulaz = document.createElement("input");
        ulaz.className = "ime";
        unosKorisnika.appendChild(ulaz);

        labela = document.createElement("label");
        labela.innerHTML = "Prezime :";
        unosKorisnika.appendChild(labela);

        ulaz = document.createElement("input");
        ulaz.className = "prezime";
        unosKorisnika.appendChild(ulaz);

        labela = document.createElement("label");
        labela.innerHTML = "Datum rodjenja :";
        unosKorisnika.appendChild(labela);

        ulaz = document.createElement("input");
        ulaz.type = "date";
        ulaz.className = "datumRodjenja";
        unosKorisnika.appendChild(ulaz);

        var dugme = document.createElement("button");
        dugme.innerHTML = "Unesite korisnika";
        unosKorisnika.appendChild(dugme);
        dugme.onclick = (ev) => {
            //sakupljanje ulaznih podataka
            const ime = unosKorisnika.querySelector(".ime").value;
            const prezime = unosKorisnika.querySelector(".prezime").value;
            const datum = unosKorisnika.querySelector(".datumRodjenja").value;
            let ind = 0;
            if(ime == "" || prezime == "" || datum == "") //proveravamo da li su podaci uneti
                alert("Niste ispravno uneli podatke korisnika.");
            else{ // ovo bi sve trebali da ispitamo na serveru !
                /*this.korisnici.forEach((el) => { //proveravamo da li korisnik vec postoji
                    if(el.ime == ime && el.prezime == prezime && el.datumRodjenja == datum)
                    {
                        alert("Korisnik vec postoji.");
                        ind = 1;
                    }
                })
                if(ind == 0)
                    this.dodajKorisnika(new Korisnik(ime,prezime,datum));*/

                fetch("https://localhost:5001/Bank/UnesiKorisnika/" + this.id, {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({
                        ime: ime,
                        prezime: prezime,
                        datumRodjenja: datum,
                    })
                }).then(p => {
                    if(p.ok){
                        // ne radimo nista valjda je korisnik unesen u banku
                        this.dodajKorisnika(new Korisnik(ime,prezime,datum));
                    }
                    else{
                        alert("Greska prilikom upisa.");
                    }
                })
            }
            
            console.log(this.korisnici);
            console.log(ime,prezime,datum);
        }
    }
    crtajUnosKredita(formaUnosa){ //zavrsena
        const unosKredita = document.createElement("div");
        formaUnosa.appendChild(unosKredita);

        var labela = document.createElement("h3");
        labela.innerHTML = "Podizanje kredita";
        unosKredita.appendChild(labela);
        unosKredita.className = "unosKredita";

        labela = document.createElement("label");
        labela.innerHTML = "Ime korisnika :";
        unosKredita.appendChild(labela);

        var ulaz = document.createElement("input");
        ulaz.className = "ime"; //mozda ce praviti problem posto se i input za unos imena korisnika isto ovako zove???
        unosKredita.appendChild(ulaz);

        labela = document.createElement("label");
        labela.innerHTML = "Unesite iznos kredita:";
        unosKredita.appendChild(labela);

        ulaz = document.createElement("input");
        ulaz.className = "iznosKredita";
        ulaz.type = "number";
        unosKredita.appendChild(ulaz);

        labela = document.createElement("label");
        labela.innerHTML = "Izaberite period otplate kredita :";
        unosKredita.appendChild(labela);

        ulaz = document.createElement("select");
        ulaz.className = "opcijePerioda";
        var opcije;
        const trenutniDatum = new Date();
        const godina = trenutniDatum.getFullYear();
        const mesec = trenutniDatum.getMonth();
        const dan = trenutniDatum.getDate();
        const formaTrenutnogDatuma = `${dan}.${mesec+1}.${godina}.`;
        const periodi = ["30 dana", "60 dana", "90 dana", "6 meseci", "1 godina"];
        const datumi = [new Date(godina,mesec,dan+30) ,new Date(godina,mesec,dan+60),new Date(godina,mesec,dan+90),new Date(godina,mesec+6,dan),new Date(godina+1,mesec,dan)];
        for( let i = 0 ; i < 5 ; i++ ){
            opcije = document.createElement("option");
            opcije.innerHTML = periodi[i];
            opcije.value = `${datumi[i].getDate()}.${datumi[i].getMonth()+1}.${datumi[i].getFullYear()}.`;
            ulaz.appendChild(opcije);
        }
        unosKredita.appendChild(ulaz);

        var dugme = document.createElement("button");
        dugme.innerHTML = "Podignite kredit";
        unosKredita.appendChild(dugme);
        dugme.onclick = (ev) => {
            //sakupljanje ulaznih podataka
            const ime = unosKredita.querySelector(".ime").value;
            const iznosKredita = parseInt(unosKredita.querySelector(".iznosKredita").value);
            const selektor = unosKredita.querySelector(".opcijePerioda");
            const datumIsplate = selektor.options[selektor.selectedIndex].value;

            if( ime == "" || unosKredita.querySelector(".iznosKredita").value == 0) //proveravamo podatke kredita
                alert("Niste uneli potrebne podatke za kredit.");
            else{ // ovo bi trebali da uradimo na serveru !
                // sad treba da proverimo da li postoji korisnik u banci sa unetim imenom
                /*let ind = 0;
                this.korisnici.forEach((el) => {
                    if(el.ime == ime)
                        ind = 1;
                });
                if( ind == 1 ) //ako je uslov ispunjen postoji korisnik u banci sa takvim imenom
                {
                    let korisnikKredita;
                    this.korisnici.forEach((el) => { //trazimo korisnika
                        if(el.ime == ime)
                            korisnikKredita = el;
                    });
                    const unetKredit = new Kredit(this.krediti.length,iznosKredita,formaTrenutnogDatuma,datumIsplate,0,korisnikKredita);
                    this.dodajKredit(unetKredit); //dodajemo kredit u banku
                    korisnikKredita.dodajKredit(unetKredit); //dodajemo kredit korisniku
                    const nosacKredita = this.container.querySelector(".platforma").querySelector(".nosacKredita");
                    unetKredit.crtajKredit(nosacKredita); //crtamo uneti kredit
                }
                else
                    alert("Korisnik sa unetim imenom ne postoji u nasoj banci.");*/
                let korisnikKredita;
                this.korisnici.forEach((el) => { //trazimo korisnika
                    if(el.ime == ime)
                        korisnikKredita = el;
                });

                fetch("https://localhost:5001/Bank/UnesiKredit/" + this.id + "/" + ime, {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({
                        broj: this.krediti.length,
                        iznos: iznosKredita,
                        datumPodizanja: formaTrenutnogDatuma,
                        datumIsplate: datumIsplate,
                        vracenIznos: 0,
                        korisnik: korisnikKredita,
                    })
                }).then(p => {
                    if(p.ok){
                        console.log(korisnikKredita);
                        let br = 0;
                        if(this.krediti.length != 0) {
                            br = this.krediti[this.krediti.length-1].broj + 1;
                        }
                        const unetKredit = new Kredit(br,iznosKredita,formaTrenutnogDatuma,datumIsplate,0,new Korisnik(korisnikKredita.ime, korisnikKredita.prezime, korisnikKredita.datumRodjenja));
                        const nosacKredita = this.container.querySelector(".platforma").querySelector(".nosacKredita");
                        this.dodajKredit(unetKredit);
                        unetKredit.crtajKredit(nosacKredita);
                    }
                    else {
                        alert("Greska prilikom upisa.");
                    }
                })
            }
            console.log(ime,iznosKredita,datumIsplate);
            console.log(this.krediti);
        }
    }
    crtajOtplatuKredita(formaUnosa){ //zavrsena
        var otplataKredita = document.createElement("div");
        formaUnosa.appendChild(otplataKredita);
        otplataKredita.className = "otplataKredita";

        var labela = document.createElement("h3");
        labela.innerHTML = "Otplata kredita";
        otplataKredita.appendChild(labela);

        labela = document.createElement("label");
        labela.innerHTML = "Unesite broj kredita koji zelite da otplatite";
        otplataKredita.appendChild(labela);

        var ulaz = document.createElement("input");
        ulaz.type = "number";
        ulaz.className = "brojKredita";
        otplataKredita.appendChild(ulaz);


        labela = document.createElement("label");
        labela.innerHTML = "Unesite sumu za otplatu kredita:";
        otplataKredita.appendChild(labela);

        ulaz  = document.createElement("input");
        ulaz.type = "number";
        ulaz.className = "sumaOtplate";
        otplataKredita.appendChild(ulaz);

        var dugme = document.createElement("button");
        dugme.innerHTML = "Otplata kredita";
        otplataKredita.appendChild(dugme);
        dugme.onclick = (ev) => {
            //sakupljanje podataka
            const brojKredita = otplataKredita.querySelector(".brojKredita").value;
            const sumaOtplate = parseInt(otplataKredita.querySelector(".sumaOtplate").value);
            
            if(brojKredita == "" || otplataKredita.querySelector(".sumaOtplate").value == "") //proveravamo da li su podaci uneti
                alert("Niste uneli potrebne podatke za otplatu kredita.");
            else{ // ovo bi trebali da odradimo na serveru !
                
                /*let ind = 0;
                this.krediti.forEach((el) => {
                    if( el.broj == brojKredita){
                        kreditZaOtplatu = el;
                        ind = 1;
                    }
                });
                if(ind == 0)
                    alert("U banci ne postoji kredit sa unesenim brojem.");
                else{ //nasli smo targetiran kredit koji sada treba da otplatimo
                    if(sumaOtplate + kreditZaOtplatu.vracenIznos > kreditZaOtplatu.iznos ) //proveravamo da ne unesemo preveliki iznos za otplatu
                        alert("Uneli ste preveliki iznos za otplatu.");
                    else{ //sve je okej trebamo sada da otplatimo kredit
                        kreditZaOtplatu.otplati(sumaOtplate); // ***!!! trebamo da definisemo novu metodu u klasi kredit za otplatu !!! ***
                        if(kreditZaOtplatu.iznos == kreditZaOtplatu.vracenIznos) //pitamo da li je kredit vracen i obrisan sa ekrana da bi ga mi obrisali i iz banke
                            delete this.krediti[kreditZaOtplatu.broj]; //brisemo kredit iz niza u banci
                    }
                    console.log(kreditZaOtplatu);
                }
                */
               let kreditZaOtplatu;
               this.krediti.forEach((el) => { 
                    if(el.broj == brojKredita)
                        kreditZaOtplatu = el;
                });

                fetch("https://localhost:5001/Bank/OtplatiKredit/" + this.id + "/" + brojKredita + "/" + sumaOtplate, {
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({
                    })
                }).then(p => {
                    if(p.ok){    
                        kreditZaOtplatu.otplati(sumaOtplate); // ***!!! trebamo da definisemo novu metodu u klasi kredit za otplatu !!! ***

                        if(kreditZaOtplatu.iznos == kreditZaOtplatu.vracenIznos) //pitamo da li je kredit vracen i obrisan sa ekrana da bi ga mi obrisali i iz banke
                            fetch("https://localhost:5001/Bank/IzbrisiKredit/" + this.id + "/" + brojKredita, {
                                method: "DELETE",
                                headers: {
                                    "Content-Type": "application/json"
                                },
                                body: JSON.stringify({
                                })
                            }).then(p => {
                                if(p.ok) {
                                    delete this.krediti[kreditZaOtplatu.broj]; //brisemo kredit iz niza u banci
                                }
                                else {
                                    alert("Greska prilikom upisa.");
                                }
                            });
                    
                        console.log(kreditZaOtplatu);
                    }
                    else {
                        alert("Greska prilikom upisa.");
                    }
                })

            }
            console.log(brojKredita,sumaOtplate);
        }
    }
}