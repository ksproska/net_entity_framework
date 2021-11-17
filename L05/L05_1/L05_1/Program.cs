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

        static (int Type, double? x0, double? x1) GetResultsTuple(string sA, string sB, string sC)
        {
            double a, b, c;
            a = StringToDoubleCaster.ToDouble(sA);
            b = StringToDoubleCaster.ToDouble(sB);
            c = StringToDoubleCaster.ToDouble(sC);

            (int Type, double? x0, double? x1) resultTuple;

            if (CheckForInfinity(a, b, c))
            {
                resultTuple = (-1, null, null);
                return resultTuple;
            }

            double[] resultValues = getXs(a, b, c);
            resultTuple = (0, null, null);
            if (resultValues.Length == 1)
            {
                resultTuple = (1, resultValues[0], null);
            }

            else if(resultValues.Length == 2)
            {   
                resultTuple = (2, resultValues[0], resultValues[1]);
            }
            
            return resultTuple;
        }

        static void Main(string[] args)
        {
            string sA, sB, sC;
            Console.WriteLine("a = ");
            sA = Console.ReadLine();
            Console.WriteLine("b = ");
            sB = Console.ReadLine();
            Console.WriteLine("c = ");
            sC = Console.ReadLine();

            int errorCode = CheckIfValid(new string[]{sA, sB, sC});
            if (errorCode != 0) return;
            Console.WriteLine($"EQUATION: {sA}x^2 + {sB}x + {sC} = 0\n");

            (int Type, double? x0, double? x1) resultTuple = GetResultsTuple(sA, sB, sC);


            const string result = "RESULT(s): ";
            if (resultTuple.Type == -1)
            {
                Console.WriteLine($"{result} infinity");
                return;
            }

            Console.WriteLine($"{result} {resultTuple.Type}");
            if(resultTuple.Type > 0)
            {
                Console.WriteLine($"x0 = {resultTuple.x0}");
            }
            if (resultTuple.Type > 1)
            {
                Console.WriteLine($"x1 = {resultTuple.x1}");
            }
        }
    }
}
