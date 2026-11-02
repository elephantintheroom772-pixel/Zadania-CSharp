
using System;
using System.Collections.Generic;
using System.IO;

string plikWejsciowy = "ranking_raw.txt";
string plikWyjsciowy = "ranking_clean.txt";

try
{
    Console.WriteLine("Katalog roboczy: " + Directory.GetCurrentDirectory());

    if (!File.Exists(plikWejsciowy))
    {
        Console.WriteLine("Blad: Nie znaleziono pliku ranking_raw.txt.");
        return;
    }

    string[] linie = File.ReadAllLines(plikWejsciowy);

    List<string> ranking = new List<string>();
    ranking.Add("Nick;Czas;Punkty;Status;Opis");

    if (linie.Length == 0)
    {
        Console.WriteLine("Plik wejsciowy jest pusty.");
    }

    for (int i = 1; i < linie.Length; i++)
    {
        if (string.IsNullOrWhiteSpace(linie[i]))
        {
            continue;
        }

        string[] pola = linie[i].Split(';');

        if (pola.Length != 5)
        {
            Console.WriteLine($"Blad w wierszu {i + 1}: nieprawidlowa liczba pol.");
            continue;
        }

        string czas = pola[1].Trim();
        string status = pola[3].Trim();

        if (!int.TryParse(pola[2].Trim(), out int punkty))
        {
            pola[2] = "0";
        }

        if (status == "HACKER" ||
            czas == "00:00:01" ||
            czas == "0:00:01")
        {
            Console.WriteLine("Odrzucono wynik: " + pola[0]);
            continue;
        }

        ranking.Add(string.Join(";", pola));
    }

    File.WriteAllLines(plikWyjsciowy, ranking);

    Console.WriteLine();
    Console.WriteLine("Oczyszczony ranking:");

    string[] wynik = File.ReadAllLines(plikWyjsciowy);

    foreach (string linia in wynik)
    {
        Console.WriteLine(linia);
    }

    Console.WriteLine();
    Console.WriteLine("Zapisano plik ranking_clean.txt.");
}
catch (IOException ex)
{
    Console.WriteLine("Blad odczytu lub zapisu pliku: " + ex.Message);
}
catch (UnauthorizedAccessException ex)
{
    Console.WriteLine("Brak uprawnien do pliku: " + ex.Message);
}
