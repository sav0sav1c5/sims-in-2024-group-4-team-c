# Projekat iz Specifikacije i modeliranja softvera (SIMS) - Platforma za smeštaj i ture

## Opis
Ovaj repozitorijum sadrži kompletan materijal za timski projektni zadatak u okviru kursa **Specifikacija i modeliranje softvera (2023/2024)**. Projekat predstavlja razvoj aplikacije za upravljanje smeštajem, turama i interakcijama između korisnika sa različitim ulogama: **vlasnik**, **gost**, **vodič** i **turista**.

Aplikacija pruža funkcionalnosti za upravljanje smeštajima, organizovanje tura, kreiranje recenzija, kao i napredne statistike koje pomažu korisnicima u donošenju poslovnih odluka.

## Specifikacije projekta

### Timska funkcionalnost
- Prijava i odjava korisnika sa prilagođenim ekranima u zavisnosti od uloge.

### Uloge i funkcionalnosti

#### 1. Vlasnik
- **Registracija smeštaja** sa detaljnim informacijama i slikama.
- **Prikaz statistika** o smeštaju (rezervacije, zauzetost, preporuke za renoviranje).
- **Predlozi za nove smeštaje** ili uklanjanje postojećih na osnovu statistike.
- **Upravljanje zahtevima za pomeranje rezervacija**.
- **Ocenjivanje gostiju** i pregled recenzija.
- **Super-vlasnik** status sa posebnim privilegijama.
- **Zakazivanje i otkazivanje renoviranja** smeštaja.
- **Komentari na forumima** specifičnim za lokaciju.

#### 2. Gost
- **Pretraga i rezervacija smeštaja** po različitim kriterijumima.
- **Ocenjivanje smeštaja i vlasnika**.
- **Upravljanje zahtevima za pomeranje i otkazivanje rezervacija**.
- **Super-gost** status sa bonus poenima za popuste.
- **Kreiranje i komentarisanje foruma** za lokacije.

#### 3. Vodič
- **Kreiranje i otkazivanje tura**.
- **Praćenje ture uživo** i označavanje prisutnih turista.
- **Prikaz i upravljanje statistikama o turama**.
- **Super-vodič** status na osnovu vođenih tura i ocena.
- **Prihvatanje zahteva za ture** (obične i složene).

#### 4. Turista
- **Pretraga i rezervacija tura**.
- **Kreiranje i praćenje zahteva za ture**.
- **Ocenjivanje ture i vodiča**.
- **Upravljanje vaučerima** za besplatne ture.

## Struktura Projekta

- `DependencyInjection/` - Konfiguracija i implementacija dependency injection-a za projekte.
- `Domain/` - Definicije domena, uključujući entitete i poslovna pravila.
`Migrations/` - Migracije baza podataka za praćenje promena u strukturi baze.
- `Resources/` - Statistički resursi kao što su slike i datoteke za lokalizaciju.
- `Utilities/` - Pomoćne klase i funkcionalnosti za projekat.
- `Diagrams/` - UML dijagrami i dokumentacija dizajna sistema.
- `DTOs/` - Data Transfer Object klase za razmenu podataka.
- `Repository/` - Implementacija repozitorijuma za pristup podacima.
- `Services/` - Sloj poslovne logike i servisnih operacija.
- `WPF/` - Korisnički interfejs i XAML fajlovi za WPF aplikaciju.
- `.gitignore` - Lista fajlova i foldera koji se ignorišu od strane Git-a.
- `GlobalConfig` - Globalna konfiguracija aplikacije.
- `App.xaml` - Definicije aplikacionih resursa za WPF.
- `BookingApp` - Glavna aplikacija za upravljanje rezervacijama.
- `AssemblyInfo` - Informacije o sklopu (assembly) projekta.

## Pokretanje Projekta

1. Klonirajte repozitorijum:
   ```bash
   git clone <repo_url>
   ```
2. Postavite bazu podataka koristeći SQL skripte iz foldera `data_modeling/`.
3. Pokrenite aplikaciju iz foldera `src/` koristeći omiljeni IDE ili komandnu liniju:
   ```bash
   python main.py
   ```
4. Instalirajte potrebne biblioteke koristeći `requirements.txt`:
   ```bash
   pip install -r requirements.txt
   ```
5. Konfigurišite postavke baze podataka u fajlu `config.properties` pre pokretanja aplikacije.

## Rezultati

- **Korisničke funkcionalnosti:** Uspešno implementirane funkcionalnosti za sve uloge korisnika (vlasnik, gost, vodič i turista).
- **Statistike:** Napredne analitičke funkcionalnosti za praćenje performansi smeštaja i tura.
- **Interfejs:** Intuitivno korisničko iskustvo sa personalizovanim funkcijama prema ulozi korisnika.

## Autori

- **Nikola Radojčić IN12/2021**  
- **Duško Radić IN39/2021**  
- **Savo Savić IN50/2021** (uloga Turista)  
- **Dragan Mišić IN59/2021**

Projekat izrađen kao deo kurseva **Specifikacija i modeliranje softvera (SIMS)** i **Human-Computer Interaction (HCI)**.