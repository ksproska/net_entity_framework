// 4.Czy zadanie 1 można wykonać z użyciem typu anonimowego w tak samo prosty sposób?

using System;

namespace L06_4
{
    class Program
    {
        static dynamic changeNazwisko(dynamic namedTuple, string newNazwisko)
        {
            namedTuple.nazwisko = newNazwisko;
            return namedTuple;
        }
        static void Main(string[] args)
        {
            var personAnn = new { imie = "Kamila", nazwisko = "Sproska", wiek = 21, placa = 1500 };
            //personAnn.nazwisko = "Kowalska";
            Console.WriteLine($"Osoba: {personAnn.nazwisko} {personAnn.imie}; Wiek: {personAnn.wiek}; Pensja: {personAnn.placa}");

            /*
            string nazwisko; double placa;
            (_, nazwisko, _, placa) = personAnn;
            Console.WriteLine($"Osoba: {nazwisko}; Pensja: {placa}");

            (var imie, _, var wiek, _) = personAnn;
            Console.WriteLine($"Osoba: {imie}; Wiek: {wiek}");
            */
        }
    }
}
