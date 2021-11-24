// 1.Stworzyć krotkę(z użyciem nawiasów okrągłych, jak na wykładzie, nie używać
//   krotek z zapisem klasy generycznej!) składającą się z danych: imię, nazwisko, wiek, 
//   płaca.Wywołaj funkcję z tą krotką jako parametrem. Na 3 sposoby pokaż możliwość 
//   skorzystania z tej krotki, aby wypisać podane informacje na konsoli.

using System;

namespace L06_1
{
    class Program
    {
        static (string imie, string nazwisko, int wiek, double placa) changeTupleContent((string imie, string nazwisko, int wiek, double placa) namedTuple, string newNazwisko)
        {
            namedTuple.nazwisko = newNazwisko;
            return namedTuple;
        }

        static void printTupleContent((string imie, string nazwisko, int wiek, double placa) namedTuple)
        {
            Console.WriteLine($"Osoba: {namedTuple.nazwisko} {namedTuple.imie}; Wiek: {namedTuple.wiek}; Pensja: {namedTuple.placa}");

            Console.WriteLine($"Osoba: {namedTuple.Item2} {namedTuple.Item1}; Wiek: {namedTuple.Item3}; Pensja: {namedTuple.Item4}");

            string nazwisko; double placa;
            (_, nazwisko, _, placa) = namedTuple;
            Console.WriteLine($"Osoba: {nazwisko}; Pensja: {placa}");

            var (imie, _, wiek, _) = namedTuple;
            Console.WriteLine($"Osoba: {imie}; Wiek: {wiek}");
        }

        static void Main(string[] args)
        {
            (string imie, string nazwisko, int wiek, double placa) namedTuple;
            namedTuple = ("Kamila", "Sproska", 21, 1500.00);
            
            namedTuple = changeTupleContent(namedTuple, "Kowalska");
            printTupleContent(namedTuple);
        }
    }
}
