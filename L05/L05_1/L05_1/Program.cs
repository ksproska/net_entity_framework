using System;
using System.Globalization;

namespace HelloWorld
{
    class Program
    {
        class StringToDoubleCaster
        {
            static NumberStyles style = NumberStyles.Number;
            static CultureInfo culture = CultureInfo.CreateSpecificCulture("en-GB");
            static bool withDotBreak = true;

            public static bool CanBeCast(string stringValue)
            {
                double temp;
                if (withDotBreak)
                    return Double.TryParse(stringValue, style, culture, out temp);
                return Double.TryParse(stringValue, out temp);
            }

            public static double ToDouble(string stringValue)
            {
                double temp;
                if (withDotBreak)
                {
                    Double.TryParse(stringValue, style, culture, out temp);
                }
                else
                {
                    Double.TryParse(stringValue, out temp);
                }
                return temp;
            }
        }

        static bool CheckForInfinity(double a, double b, double c)
        {
            if (a == 0 && b == 0 && c == 0) return true;
            return false;
        }

        static double[] getXs(double a, double b, double c)
        {
            if (a == 0 && b == 0)
            {
                return new double[0] { };
            }
            if (a == 0)
            {
                double x = -c / b;
                return new double[1] { x };
            }

            double delta = CountDelta(a, b, c);
            Console.WriteLine(delta);
            if (delta > 0)
            {
                double x1 = (-b - Math.Sqrt(delta)) / (2 * a);
                double x2 = (-b + Math.Sqrt(delta)) / (2 * a);
                return new double[2] { x1, x2 };
            }
            else if (delta == 0)
            {
                double x = -b / (2 * a);
                return new double[1] { x };
            }
            return new double[0] { };
        }

        static int CheckIfValid(string[] args)
        {
            if (args.Length != 3) return 1;
            for (int i = 0; i < args.Length; i++)
            {
                if (!StringToDoubleCaster.CanBeCast(args[i]))
                {
                    return i + 2;
                }
            }
            return 0;
        }

        static double CountDelta(double a, double b, double c)
        {
            return (b * b) - (4 * a * c);
        }

        static void Main(string[] args)
        {
            int errorCode = CheckIfValid(args);
            if (errorCode != 0) return;

            double a, b, c;
            a = StringToDoubleCaster.ToDouble(args[0]);
            b = StringToDoubleCaster.ToDouble(args[1]);
            c = StringToDoubleCaster.ToDouble(args[2]);

            Console.WriteLine($"EQUATION: {a}x^2 + {b}x + {c} = 0\n");

            const string result = "RESULT(s): ";
            if (CheckForInfinity(a, b, c))
            {
                Console.WriteLine($"{result} infinity");
                return;
            }

            double[] resultValues = getXs(a, b, c);
            Console.WriteLine($"{result} {resultValues.Length}");
            for (int i = 0; i < resultValues.Length; i++)
            {
                Console.WriteLine($"x{i} = {resultValues[i]}");
            }
            // test pushing from lower folder
        }
    }
}
