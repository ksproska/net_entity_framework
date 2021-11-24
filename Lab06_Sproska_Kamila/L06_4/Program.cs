// 4.Czy zadanie 1 można wykonać z użyciem typu anonimowego w tak samo prosty sposób?

using System;

namespace L06_4
{
    class Program
    {
        static void printPersonAnn(dynamic personAnn)
        {
            Console.WriteLine($"Osoba: {personAnn.nazwisko} {personAnn.imie}; Wiek: {personAnn.wiek}; Pensja: {personAnn.placa}");
        }

        static void Main(string[] args)
        {
            var personAnn = new { imie = "Kamila", nazwisko = "Sproska", wiek = 21, placa = 1500 };
            printPersonAnn(personAnn);

        }
    }
}
