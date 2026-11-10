using System.Collections.Immutable;

namespace JatekProjekt;

public readonly struct KarakterElemzo
{
    private readonly List<Karakter> karakterek;

    // Konstruktor: példányosítja az adatbázis-kezelőt és betölti az adatokat a listába
    public KarakterElemzo()
    {
        var dbKezelo = new AdatbazisKezelo();
        karakterek = dbKezelo.AdatokBetoltese();
    }

    public List<string> MagasSzintuKarakterek() => karakterek.Where(x => x.Szint >= 40).Select(x => x.Nev).ToList();

    // Gyűjtsd ki az összes "Mágus" osztályú karakter nevét!
    public List<string> MagusOsztalyuNevek() => karakterek.Where(x => x.Osztaly.Equals("Mágus")).Select(x => x.Nev).ToList();

    // Add vissza az összes karakter nevét egy egyszerű listában!
    public List<string> OsszesKarakterNevListaba() => karakterek.Select(x => x.Nev).ToList();

    // Szűrd ki azokat a karaktereket, amelyek valamelyik "EU-"-val kezdődő szerveren játszanak, és add vissza a nevüket listában!
    public List<string> EuSzerveresKarakterek() => karakterek.Where(x => x.Szerver.StartsWith("EU")).Select(x => x.Nev).ToList();

    // Válaszd ki az összes egyedi (különböző) szervernevet, ahol a karakterek játszanak (Distinct segítségével)!
    public List<string> EgyediSzerverNevek() => karakterek.DistinctBy(x => x.Szerver).Select(x => x.Szerver).ToList();

    // ==========================================
    // 2. EGYSZERŰ AGGREGÁCIÓK ÉS ELDÖNTÉsek
    // ==========================================

    // Számold meg Sum segítségével, hogy mennyi a karakterek összes aranya a szerveren!
    public int OsszesAranyMennyiseg() => karakterek.Sum(x => x.Arany);

    // Számítsd ki Average segítségével a karakterek átlagos szintjét double értékként!
    public double AtlagSzint() => karakterek.Average(x => x.Szint);

    // Állapítsd meg Any segítségével, hogy van-e olyan "Tolvaj" osztályú karakter, akinek a szintje eléri vagy meghaladja a 40-et!
    public bool VaneMagasSzintuTolvaj() => karakterek.Any(x => x.Osztaly.Equals("Tolvaj") && x.Szint >= 40);

    // Ellenőrizd Any segítségével, hogy létezik-e olyan karakter, akinek 10 000 aranynál több van a zsebében!
    public bool VanETizEzerAranynalTobb() => karakterek.Any(x => x.Arany > 10_000);

    // Keresd meg FirstOrDefault segítségével az első olyan karaktert, akinek az aranya meghaladja az 5000-et!
    public Karakter LegelsoOvenKarakter() => karakterek.FirstOrDefault(x => x.Arany > 5000);

    // ==========================================
    // 3. RENDEZÉsek ÉS KERESÉsek
    // ==========================================

    // Add vissza az összes karaktert szint szerint csökkenő, azon belül pedig név szerint növekvő sorrendbe rendezve!
    public List<Karakter> KarakterekSzintSzerintRendezve() => karakterek.OrderByDescending(x => x.Szint).ThenBy(x => x.Nev).ToList();

    // Keresd meg OrderByDescending és FirstOrDefault segítségével annak a karakternek a nevét, akinek a legtöbb aranya van!
    public string LeggazdagabbKarakterNeve() => karakterek.OrderByDescending(x => x.Arany).FirstOrDefault().Nev;

    // Keresd meg OrderBy és FirstOrDefault segítségével a legszegényebb (legkevesebb aranyú) karakter nevét!
    public string LegszegenyebbKarakterNeve() => karakterek.OrderBy(x => x.Arany).FirstOrDefault().Nev;

    // Keresd meg Max segítségével a legmagasabb szintű karakter szintjét!
    public int LegmagasabbSzint() => karakterek.Max(x => x.Szint);

    // Rendezd a karaktereket a nevük hossza szerint növekvő sorrendbe!
    public List<Karakter> KarakterekNevHosszaSzerint() => karakterek.OrderBy(x => x.Nev.Length).ToList();

    // ==========================================
    // 4. CSOPORTOSÍTÁS ÉS ÖSSzetettebb LINQ MŰVELETek
    // ==========================================

    // Csoportosítsd a karaktereket az Osztaly tulajdonságuk alapján, és alakítsd át ToDictionary-vel úgy, hogy a kulcs az osztály neve, az érték pedig a karakterek darabszáma legyen!
    public Dictionary<string, int> OsztalyonkentKarakterSzam() => karakterek.GroupBy(x => x.Osztaly).Select(x => new KeyValuePair<string, int>(x.Key, x.Count())).ToDictionary();

    // Csoportosítsd a karaktereket a Szerver tulajdonságuk alapján, és számold meg, hány karakter játszik az egyes szervereken!
    public Dictionary<string, int> SzerverenKarakterekSzama() => karakterek.GroupBy(x => x.Szerver).Select(x => new KeyValuePair<string, int>(x.Key, x.Count())).ToDictionary();

    // Gyűjtsd ki az összes "Harcos" osztályú karakter azonosítóját (Id) egy listába!
    public List<int> HarcosokAzonositoi() => karakterek.Where(x => x.Osztaly.Equals("Harcos")).Select(x => x.Id).ToList();

    // Állapítsd meg, hogy van-e olyan 50-es szintnél magasabb Mágus, akinek 3000-nél kevesebb aranya van!
    public bool VanETolvajSpecKondicioval() => karakterek.Exists(x => x.Szint > 50 && x.Arany < 3000);

    // Keresd meg és add vissza a legmagasabb szintű karakter objektumát!
    public Karakter LegmagasabbSzintuKarakterObjektum() => karakterek.MaxBy(x => x.Szint);
}
