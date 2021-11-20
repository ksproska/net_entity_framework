// 1.Stworzyć krotkę(z użyciem nawiasów okrągłych, jak na wykładzie, nie używać
//   krotek z zapisem klasy generycznej!) składającą się z danych: imię, nazwisko, wiek, 
//   płaca.Wywołaj funkcję z tą krotką jako parametrem. Na 3 sposoby pokaż możliwość 
//   skorzystania z tej krotki, aby wypisać podane informacje na konsoli.

using System;

namespace L06_1
{
    class Program
    {
        static (string imie, string nazwisko, int wiek, double placa) printTupleContent((string imie, string nazwisko, int wiek, double placa) namedTuple, string newNazwisko)
        {
            namedTuple.nazwisko = newNazwisko;
            return namedTuple;
        }

        static void Main(string[] args)
        {
            (string imie, string nazwisko, int wiek, double placa) namedTuple;
            namedTuple = ("Kamila", "Sproska", 21, 1500.00);
            
            namedTuple = printTupleContent(namedTuple, "Kowalska");
            Console.WriteLine($"Osoba: {namedTuple.nazwisko} {namedTuple.imie}; Wiek: {namedTuple.wiek}; Pensja: {namedTuple.placa}");


            string nazwisko; double placa;
            (_, nazwisko, _, placa) = namedTuple;
            Console.WriteLine($"Osoba: {nazwisko}; Pensja: {placa}");

            (var imie, _, var wiek, _) = namedTuple;
            Console.WriteLine($"Osoba: {imie}; Wiek: {wiek}");
        }
    }
}
