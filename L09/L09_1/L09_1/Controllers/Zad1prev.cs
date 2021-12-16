using System;
using System.Globalization;

namespace Zad1prev
{
    class Zad1prev
    {
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
            //Console.WriteLine(delta);
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

        static double CountDelta(double a, double b, double c)
        {
            return (b * b) - (4 * a * c);
        }

        public static (int Type, double? x0, double? x1) GetResultsTuple(double a, double b, double c)
        {   
            (int Type, double? x0, double? x1) resultTuple;

            if (CheckForInfinity(a, b, c))
            {
                resultTuple = (-1, null, null);
                return resultTuple;
            }

            double[] resultValues = getXs(a, b, c);
            resultTuple = (0, null, null);
            int significantDig = 5;
            if (resultValues.Length == 1)
            {
                //resultTuple = (1, resultValues[0], null);
                resultTuple = (1, RoundToSignificantDigits(resultValues[0], significantDig), null);
            }

            else if(resultValues.Length == 2)
            {
                //resultTuple = (2, resultValues[0], resultValues[1]);
                resultTuple = (2, RoundToSignificantDigits(resultValues[0], significantDig), 
                    RoundToSignificantDigits(resultValues[1], significantDig));
            }
            
            return resultTuple;
        }

        static double RoundToSignificantDigits(double? td, int digits)
        {
            double d = (double)td;
            if (d == 0)
                return 0;
            int innDigits = digits - (int)Math.Floor(Math.Log10(Math.Abs(d))) - 1;
            /*if((int)Math.Floor(Math.Abs(d)) == 0)
            {
                innDigits += 1;
            }*/
            return Math.Round(d, innDigits);
        }

        public static String GetEquation(double iA, double iB, double iC)
        {
            String equ = "";
            if (iA != 0 && Math.Abs(iA) != 1)
            {
                equ = $"{iA}x^2";
            }
            else if(iA != 0)
            {
                equ = $"x^2";
                if(iA == -1)
                {
                    equ = $"-x^2";
                }
            }
            if (iB != 0)
            {
                if (equ.Length > 0)
                {
                    if(iB > 0)
                    {
                        equ += " + ";
                    }
                    else
                    {
                        equ += " - ";
                    }
                }
                if(Math.Abs(iB) != 1)
                {
                    equ += $"{Math.Abs(iB)}x";
                }
                else
                {
                    equ += $"x";
                }
            }
            if (iC != 0)
            {
                if (equ.Length > 0)
                {
                    if (iC > 0)
                    {
                        equ += " + ";
                    }

                    else
                    {
                        equ += " - ";
                    }
                }
                equ += $"{Math.Abs(iC)}";
            }
            if (equ.Length == 0)
            {
                equ = "0";
            }
            return equ;
        }

        //static void Main(string[] args)
        //{
        //    (int Type, double? x0, double? x1) resultTuple = GetResultsTuple(1, 1, 1);
        //    if (resultTuple.Type < -1)
        //    {
        //        Console.WriteLine($"Arguments must be dubble.");
        //        return;
        //    }

        //    const string result = "RESULT(s): ";
        //    if (resultTuple.Type == -1)
        //    {
        //        Console.WriteLine($"{result} infinity");
        //        return;
        //    }

        //    Console.WriteLine($"{result} {resultTuple.Type}");
        //    if(resultTuple.Type > 0)
        //    {
        //        Console.WriteLine("x0 = {0}", RoundToSignificantDigits(resultTuple.x0, 5).ToString());
        //    }
        //    if (resultTuple.Type > 1)
        //    {
        //        Console.WriteLine($"x1 = {RoundToSignificantDigits(resultTuple.x1, 5)}");
        //    }
        //}
    }
}
