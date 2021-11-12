using System;

namespace L05_2
{

    class Program
    {
        static int? GetSemiMaxRecurrent(string input, int left_n, int max_val, int semi_max_val)
        {
            if (left_n == 0)
            {
                if (max_val == semi_max_val) return null;
                return semi_max_val;
            }
            int next_val;
            if (left_n == 1)
            {
                next_val = int.Parse(input);
                input = "";
            }
            else
            {
                string valuePart = input.Split(' ', '\t', '\n')[0];
                //Console.WriteLine(valuePart);
                next_val = int.Parse(valuePart);
                input = input.Substring(valuePart.Length + 1);
                //Console.WriteLine(input);
            }

            if (max_val < next_val)
            {
                semi_max_val = max_val;
                max_val = next_val;
            }
            else if (semi_max_val < next_val)
            {
                semi_max_val = next_val;
            }
            return GetSemiMaxRecurrent(input, left_n - 1, max_val, semi_max_val);
        }
        static int? GetSemiMax(string input, int n)
        {
            string valuePart = input.Split(' ', '\t', '\n')[0];
            int next_val = int.Parse(valuePart);
            return GetSemiMaxRecurrent(input, n, next_val, next_val);
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
            string n_arguments = Console.ReadLine();
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
            Console.WriteLine(GetSemiMax(n_arguments, n));
        }
    }
}
