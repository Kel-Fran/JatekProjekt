namespace JatekProjekt;

public record struct Karakter(int Id, string Nev, string Osztaly, int Szint, int Arany, string Szerver)
{
    public override string ToString() => $"{Nev} [{Osztaly}] - Szint: {Szint} | Arany: {Arany} | Szerver: {Szerver}";
}
