/*
6.  Stworzyć metodę CountMyTypes, w której parametrach można podać dowolny ciąg elementów  rozdzielonych przecinkami, a metoda ta 
zliczy ile wśród parametrów było parzystych liczb całkowitych, liczb rzeczywistych dodatnich, napisów co najmniej 5-znakowych  i  
elementów  innych  typów  niż  w/w,  zwracając  te  cztery  wartości  w wybrany sposób. Użyć instrukcji switch/case.
 */
using System;

namespace L06_6
{
    class Program
    {
        static (int evenIntegers, int positiveReal, int moreThan5SignStrings, int other) CountMyTypes(params dynamic[] multipleTypes)
        {
            (int evenIntegers, int positiveReal, int moreThan5SignStrings, int other) counterTuple = (0, 0, 0, 0);

            foreach(dynamic line in multipleTypes)
            {
                switch (line)
                {
                    case double f when f >= 0:
                        counterTuple.positiveReal += 1;
                        break;
                    case int i when i % 2 == 0:
                        counterTuple.evenIntegers += 1;
                        break;
                    case string s when s.Length >= 5:
                        counterTuple.moreThan5SignStrings += 1;
                        break;
                    default:
                        counterTuple.other += 1;
                        break;
                }
            }
            return counterTuple;
        }
        static void Main(string[] args)
        {
            var namedTuple = CountMyTypes("a", "abcde", 4, 4, 3, 2.3, -1.2, 23.8278479873);
            Console.WriteLine($"(int evenIntegers, int positiveReal, int moreThan5SignStrings, int other): {namedTuple}");

        }
    }
}
