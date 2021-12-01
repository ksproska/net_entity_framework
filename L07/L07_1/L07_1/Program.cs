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

        public MixedNumber(bool sign, int natural, int numerator, int denominator)
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

        public void PrintAll()
        {
            Console.WriteLine($"{ToString()} = {ToDouble()}  <=  (sign:{Positive}; natural:{natural}; numerator:{numerator}; denominator:{denominator})");
        }

        public MixedNumber(int natural, bool sign = true) : this(sign, natural, 0, 1) { }
        public MixedNumber(int numerator, int denominator, bool sign = true) : this(sign, 0, numerator, denominator) { }

        private static int gcd(int a, int b)
        {
            if (b == 0) return a;
            return gcd(b, a % b);
        }

        private bool SimplifyMixedNumber()
        {
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
                int gcdVal = MixedNumber.gcd(numerator, denominator);
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
                return new MixedNumber(n1.Positive, 0, n1_numerator + n2_numerator, denominator);
            }
            if (n1_numerator == n2_numerator)
            {
                return new MixedNumber(true, 0, 0, 1);
            }
            
            if (n1_numerator > n2_numerator)
            {
                return new MixedNumber(n1.Positive, 0, Math.Abs(n1_numerator - n2_numerator), denominator);
            }
            return new MixedNumber(n2.Positive, 0, Math.Abs(n1_numerator - n2_numerator), denominator);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //MixedNumber mx1 = new MixedNumber(3, 3, 35);
            //mx1.PrintAll();
            //MixedNumber mx2 = new MixedNumber(3, 4, 512, false);
            //mx1.PrintAll();
            //MixedNumber mx3 = new MixedNumber(1, 7);
            //mx1.PrintAll();
            //MixedNumber mx4 = new MixedNumber(8, 17, false);
            //mx1.PrintAll();
            //MixedNumber mx5 = new MixedNumber(3);
            //mx1.PrintAll();
            //MixedNumber mx6 = new MixedNumber(5, false);

            MixedNumber mx = new MixedNumber(true, 1, 35,  3);
            Console.WriteLine(mx);
            mx.PrintAll();
            MixedNumber mx2 = new MixedNumber(36);
            Console.WriteLine(mx2);
            Console.WriteLine(mx + mx2);

        }
    }
}
