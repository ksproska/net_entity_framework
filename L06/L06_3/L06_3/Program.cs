// 3.  Zaprezentuj użycie wybranych 5 różnych metod z modułu System.Arrays.

using System;

namespace L06_3
{
    class Program
    {
        static void printTable(Array myarray)
        {
            foreach (var i in myarray)
            {
                if(i == null)
                {
                    Console.Write($"null\t");
                }
                else
                {
                    Console.Write($"{i}\t");
                }
            }
            Console.WriteLine("");
        }
        static void Main(string[] args)
        {
            // 1.
            Console.WriteLine("CreateInstance: len=9; Copy: len=6");
            int?[] tableOfInt = { 4, 9, 3, 7, 6, 4, 5, 5};
            Array myArrayTwo = Array.CreateInstance(typeof(int?), 9);
            Array.Copy(tableOfInt, myArrayTwo, 6);
            
            printTable(tableOfInt);
            printTable(myArrayTwo);
            Console.WriteLine("--------------------------------------------------------------------------");

            // 2.
            Console.WriteLine("Resize: len=10");
            Array.Resize(ref tableOfInt, 10);
            printTable(tableOfInt);
            Console.WriteLine("--------------------------------------------------------------------------");

            // 3.
            Console.WriteLine("Find:");
            int? findMoreThan5 = Array.Find(tableOfInt, p => p > 5);
            Console.WriteLine($"findMoreThan5: {findMoreThan5}");
            int?[] findAllMore3 = Array.FindAll(tableOfInt, p => p > 3);
            Console.WriteLine($"findAllMore3:");
            printTable(findAllMore3);
            Console.WriteLine("--------------------------------------------------------------------------");

            // 4.
            Console.WriteLine("Sort:");
            Array.Sort(tableOfInt);
            printTable(tableOfInt);
            Console.WriteLine("--------------------------------------------------------------------------");

            // 5.
            Console.WriteLine("Fill: val=88");
            Array.Fill(tableOfInt, 88);
            printTable(tableOfInt);
            
        }
    }
}
