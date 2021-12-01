using System;

namespace L07_1
{
    class MixedNumber
    {
        private static int simplificationCounter = 0;
        public bool Positive { get; set; }  = true;
        private int natural, numerator, denominator; // get set dla set domyslna, z duzej litery

        public int Natural
        {
            get { return natural; }
            set { if (value < 0) natural = 0; else natural = value; }
        }
        public int Numerator
        {
            get { return numerator; }
            set { if (value < 0) numerator = 0; else numerator = value; }
        }
        public int Denominator
        {
            get { return denominator; }
            set { if (value <= 0) denominator = 1; else denominator = value; }
        }

        public MixedNumber(int natural, int numerator, int denominator, bool sign = true)
        {
            this.Positive = sign;
            this.Natural = natural;
            this.Numerator = numerator;
            this.Denominator = denominator;

            if(SimplifyMixedNumber())
            {
                simplificationCounter += 1;
            }
        }

        public MixedNumber(double doubleValue, bool sign = true, int precision = 5)
        {
            this.Positive = sign;
            this.Natural = 0;
            this.Numerator = (int)Math.Floor(doubleValue*Math.Pow(10, precision));
            this.Denominator = (int)Math.Pow(10, precision);

            if (SimplifyMixedNumber())
            {
                simplificationCounter += 1;
            }
        }

        public void PrintAll()
        {
            Console.WriteLine($"{ToString().PadRight(15)} = {ToDouble()}".PadRight(45) + $"<=  ({Positive}\t {natural}, {numerator}, {denominator})");
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

            return operationsFlag;
        }

        public override string ToString()
        {
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

            if (!Positive)
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
            if(!Positive)
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


            if (n1.Positive == n2.Positive)
            {
                return new MixedNumber(0, n1_numerator + n2_numerator, denominator, n1.Positive);
            }
            if (n1_numerator == n2_numerator)
            {
                return new MixedNumber( 0, 0, 1, true);
            }
            
            if (n1_numerator > n2_numerator)
            {
                return new MixedNumber( 0, Math.Abs(n1_numerator - n2_numerator), denominator, n1.Positive);
            }
            return new MixedNumber(0, Math.Abs(n1_numerator - n2_numerator), denominator, n2.Positive);
        }

        public void Deconstruct(out bool sign, out int natural, out int numerator, out int denominator)
        {
            (sign, natural, numerator, denominator) = (this.Positive, this.Natural, this.Numerator, this.Denominator);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            MixedNumber mx1 = new MixedNumber(4, 3, 35);
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

            Console.WriteLine(mx1 + mx2);
            Console.WriteLine(mx3 + mx4 + mx5);

            bool s;
            int num;
            mx1.Deconstruct(out s, out num, out _, out int dec);
            Console.WriteLine($"{s} - {num} - {dec}");
        }
    }
}
