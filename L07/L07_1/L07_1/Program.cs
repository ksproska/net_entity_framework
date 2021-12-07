using System;

namespace L07_1
{
    class MixedNumber
    {
        public bool IsPositive { get; set; }  = true;
        private int natural, numerator, denominator;
        private static int defNatural = 0, defNumerator = 0, defDenominator = 1;

        public static int SimplificationCounter { get; private set; }

        public int Natural
        {
            get { return natural; }
            set { if (value < 0) natural = defNatural; else natural = value; }
        }
        public int Numerator
        {
            get { return numerator; }
            set { numeratorSetterWithDefault(value); if (SimplifyMixedNumber()) { SimplificationCounter += 1; } }
        }
        public int Denominator
        {
            get { return denominator; }
            set { denominatorSetterWithDefault(value); if (SimplifyMixedNumber()) { SimplificationCounter += 1; } }
        }

        private void numeratorSetterWithDefault(int value)
        {
            if (value < 0) numerator = defNumerator; else numerator = value;
        }

        private void denominatorSetterWithDefault(int value)
        {
            if (value <= 0) denominator = defDenominator; else denominator = value;
        }

        public MixedNumber(int natural, int numerator, int denominator, bool sign = true)
        {
            this.IsPositive = sign;
            this.Natural = natural;
            numeratorSetterWithDefault(numerator);
            denominatorSetterWithDefault(denominator);

            if(SimplifyMixedNumber()) { SimplificationCounter += 1; }
        }

        public MixedNumber(double doubleValue, bool sign = true, int precision = 5)
        {
            this.IsPositive = sign;
            this.Natural = 0;
            int tempNumerator = (int)Math.Floor(doubleValue*Math.Pow(10, precision));
            int tempDenumerator = (int)Math.Pow(10, precision);
            numeratorSetterWithDefault(tempNumerator);
            denominatorSetterWithDefault(tempDenumerator);

            if (SimplifyMixedNumber()) { SimplificationCounter += 1; }
        }

        public void PrintAll()
        {
            Console.WriteLine($"{SimplificationCounter}".PadRight(4) + $"{ToString().PadRight(15)} = {ToDouble()}".PadRight(45) + $"<=  ({IsPositive}\t {natural}, {numerator}, {denominator})");
        }

        public MixedNumber(int natural, bool sign = true) : this(natural, 0, 1, sign) { }
        public MixedNumber(int numerator, int denominator, bool sign = true) : this(0, numerator, denominator, sign) { }

        private bool SimplifyMixedNumber()
        {
            int gcd(int a, int b)
            {
                if (b == 0) return a;
                return gcd(b, a % b);
            }

            bool operationsFlag = false;
            while(numerator >= denominator)
            {
                numerator -= denominator;
                natural += 1;
                operationsFlag = true;
            }
            if (numerator == 0)
            {
                if (denominator != 1)
                {
                    operationsFlag = true;
                    denominator = 1;
                }
            }
            else
            {
                int gcdVal = gcd(numerator, denominator);
                if (gcdVal != 1)
                {
                    numerator /= gcdVal;
                    denominator /= gcdVal;
                    operationsFlag = true;
                }
            }
            if (numerator == 0 && natural == 0)
            {
                this.IsPositive = true;
            }

            return operationsFlag;
        }

        public override string ToString()
        {
            if (natural == 0 && numerator == 0)
            {
                return "0";
            }
            string sToString = "";
            if (natural != 0 & numerator != 0)
            {
                sToString = $"{natural} {numerator}/{denominator}";
            }
            else if (natural == 0)
            {
                sToString = $"{numerator}/{denominator}";
            }
            else
            {
                sToString = $"{natural}";
            }

            if (!IsPositive)
            {
                if(natural != 0 & numerator != 0)
                {
                    sToString = $"-({sToString})";
                }
                else
                {
                    sToString = $"-{sToString}";
                }
                
            }
            return sToString;
        }

        public double ToDouble()
        {
            double doubleVal = natural;
            doubleVal += (double)numerator / denominator;
            if(!IsPositive)
            {
                doubleVal *= -1;
            }
            return doubleVal;
        }

        public static MixedNumber operator +(MixedNumber n1, MixedNumber n2) {
            int n1_numerator = n1.numerator * n2.denominator;
            int n2_numerator = n2.numerator * n1.denominator;
            int denominator = n1.denominator * n2.denominator;
            n1_numerator += n1.natural * denominator;
            n2_numerator += n2.natural * denominator;
            // natural is eqal 0 (value is contained in numerator)


            if (n1.IsPositive == n2.IsPositive)
            {
                return new MixedNumber(0, n1_numerator + n2_numerator, denominator, n1.IsPositive);
            }
            if (n1_numerator == n2_numerator)
            {
                return new MixedNumber( 0, 0, 1, true);
            }
            
            if (n1_numerator > n2_numerator)
            {
                return new MixedNumber( 0, Math.Abs(n1_numerator - n2_numerator), denominator, n1.IsPositive);
            }
            return new MixedNumber(0, Math.Abs(n1_numerator - n2_numerator), denominator, n2.IsPositive);
        }

        public void Deconstruct(out bool sign, out int natural, out int numerator, out int denominator)
        {
            (sign, natural, numerator, denominator) = (this.IsPositive, this.Natural, this.Numerator, this.Denominator);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            MixedNumber mx1 = new MixedNumber(4, 3, 35);
            mx1.Numerator = 5;
            mx1.PrintAll();
            MixedNumber mx2 = new MixedNumber(3, 4, 52, false);
            mx2.PrintAll();
            MixedNumber mx3 = new MixedNumber(1, 7);
            mx3.PrintAll();
            MixedNumber mx4 = new MixedNumber(8, 17, false);
            mx4.PrintAll();
            MixedNumber mx5 = new MixedNumber(3);
            mx5.PrintAll();
            MixedNumber mx6 = new MixedNumber(5, false);
            mx6.PrintAll();
            MixedNumber mx7 = new MixedNumber(3.34);
            mx7.PrintAll();
            MixedNumber mx8 = new MixedNumber(5.25304, false);
            mx8.PrintAll();
            
            Console.WriteLine($"\n{mx1} + {mx2} = {mx1 + mx2}");
            Console.WriteLine($"\n{mx3} + {mx4} + {mx5} = {mx3 + mx4 + mx5}");
            Console.WriteLine($"\n{mx1} + {mx3} + {mx6} = {mx1 + mx3 + mx6}");

            var (s, num, _, dec) = mx1;
            
            Console.WriteLine($"\n{s} - {num} - {dec}\n");

            Console.WriteLine(MixedNumber.SimplificationCounter);


            Console.WriteLine($"\n{new MixedNumber(0) + new MixedNumber(6, 5)}");
            Console.WriteLine($"{new MixedNumber(10, 3, 2) + new MixedNumber(1, 6)}");
            Console.WriteLine($"{new MixedNumber(8, 1, 5, false) + new MixedNumber(1, 1, 5, false)}");
            Console.WriteLine($"{new MixedNumber(10, 7, false) + new MixedNumber(1, 6)}");
        }
    }
}
