export class Korisnik {
    constructor(ime, prezime, datumRodjenja ){
        this.ime = ime;
        this.prezime = prezime;
        this.datumRodjenja = datumRodjenja;
        this.krediti = [];
    }
    dodajKredit(kredit){
        this.krediti.push(kredit);
    }
    vratiPodatke(){
        return `${this.ime} ${this.prezime} ${this.datumRodjenja}`;
    }
}