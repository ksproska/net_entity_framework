using System;

namespace L05_2
{

    class Program
    {
        static int? GetSemiMax(int n)
        {
            (int? max_val, int? semi_max_val) resultTuple = (null, null);
            int counter = 0;
            char ch;
            string previous = "";
            int nextInt;
            while (counter < n)
            {
                nextInt = Console.Read();


                ch = Convert.ToChar(nextInt);
                if (Char.IsWhiteSpace(ch))
                {

                    if (previous.Length > 0)
                    {
                        int val_read = int.Parse(previous);
                        previous = "";
                        counter++;

                        if (resultTuple.max_val == null || val_read > resultTuple.max_val)
                        {
                            resultTuple = (val_read, resultTuple.max_val);
                        }
                        else if (resultTuple.semi_max_val != null && val_read > resultTuple.semi_max_val)
                        {
                            resultTuple = (resultTuple.max_val, val_read);
                        }
                    }

                }
                else
                {
                    previous += ch;
                }
            }
            return resultTuple.semi_max_val;
        }
        static void Main(string[] args)
        {
            /*
            if (args.Length == 0)
            {
                Console.WriteLine($"no atributes given");
                return;
            }
            
            */
            Console.WriteLine("How many arguments? n >= 1");
            string n_str = Console.ReadLine();
            int n = int.Parse(n_str);
            if (n < 1)
            {
                Console.WriteLine($"n must be >=1, given: {n}");
                return;
            }

            Console.WriteLine($"Pass {n} arguments (ints):");
            int? semiMax = GetSemiMax(n);

            if (semiMax == null)
            {
                Console.WriteLine("Brak rozwiązania.");
                return;
            }
            Console.WriteLine($"{semiMax}");

            /*
            int max_val = int.Parse(args[1]);
            int semi_max_val = max_val;

            for (int i = 2; i < args.Length; i++)
            {
                int next_val = int.Parse(args[i]);
                if(next_val > max_val)
                {
                    semi_max_val = max_val;
                    max_val = next_val;
                }
            }
            */
        }
    }
}
