using System;
using System.Collections.Generic;

class MainClass
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Unesite broj n:");
        int n = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Unesite vrednost M:");
        int M = Convert.ToInt32(Console.ReadLine());

        int[] brojevi = new int[n+1];

        Console.WriteLine("Unesite n brojeva:");
        brojevi[0] = 0;
        for (int i = 1; i < n+1; i++)
        {
            brojevi[i] = Convert.ToInt32(Console.ReadLine());
        }

        List<List<int>> kombinacije = GenerisiKombinacije(brojevi);

        foreach (List<int> kombinacijaBrojeva in kombinacije) { 
            List<string> izrazi = GenerisiIzraze(kombinacijaBrojeva.ToArray());

            foreach (string izraz in izrazi)
            {
                if (Izracunaj(izraz) == M)
                {
                    Console.WriteLine("Izraz koji daje vrijednost: " + M + " je izraz: " + izraz.Remove(0, 1));
                    return;
                }
            }
        }

        Console.WriteLine("Nije moguće generisati izraz sa zadatom vrednošću M.");
    }

    static List<string> GenerisiIzraze(int[] brojevi)
    {
        List<string> izrazi = new List<string>();
        int n = brojevi.Length;

        for (int i = 0; i < (1 << (n - 1)); i++)
        {
            string izraz = brojevi[0].ToString();

            for (int j = 0; j < n - 1; j++)
            {
                if ((i & (1 << j)) > 0)
                {
                    izraz += "+" + brojevi[j + 1];
                }
                else
                {
                    izraz += "-" + brojevi[j + 1];
                }
            }

            izrazi.Add(izraz);
        }

        return izrazi;
    }

    static int Izracunaj(string izraz)
    {
        string[] dijelovi = izraz.Split(new char[] { '+', '-' }, StringSplitOptions.None);
        char[] znakovi = new char[dijelovi.Length - 1];
        int j = 0;

        foreach (char znak in izraz)
        {
            if (znak == '+' || znak == '-')
            {
                znakovi[j] = znak;
                j++;
            }
        }

        int rezultat = int.Parse(dijelovi[0]);

        for (int i = 1; i < dijelovi.Length; i++)
        {
            if (znakovi[i - 1] == '+')
            {
                rezultat += int.Parse(dijelovi[i]);
            }
            else
            {
                rezultat -= int.Parse(dijelovi[i]);
            }
        }

        return rezultat;
    }

    static List<List<int>> GenerisiKombinacije(int[] brojevi)
    {
        List<List<int>> kombinacije = new List<List<int>>();
        GenerisiKombinacijeHelper(brojevi, 0, new List<int>(), kombinacije);
        return kombinacije;
    }

    static void GenerisiKombinacijeHelper(int[] brojevi, int index, List<int> trenutnaKombinacija, List<List<int>> kombinacije)
    {
        if (index >= brojevi.Length)
        {
            kombinacije.Add(new List<int>(trenutnaKombinacija));
            return;
        }

        GenerisiKombinacijeHelper(brojevi, index + 1, trenutnaKombinacija, kombinacije);

        trenutnaKombinacija.Add(brojevi[index]);
        GenerisiKombinacijeHelper(brojevi, index + 1, trenutnaKombinacija, kombinacije);
        trenutnaKombinacija.RemoveAt(trenutnaKombinacija.Count - 1);
    }
}
