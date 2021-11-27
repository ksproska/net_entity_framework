using System;

namespace L07_3
{
    public static class StringExtended
    {
        public static string ChangeCaseEverySecond(this string str)
        {
            char[] charTab = str.ToCharArray();
            for(int i=0; i<charTab.Length; i++)
            {
                if (!char.IsLetter(charTab[i]))
                {
                    charTab[i] = '.';
                }
                else if(i%2 == 0)
                {
                    charTab[i] = char.ToUpper(charTab[i]);
                }
                else
                {
                    charTab[i] = char.ToLower(charTab[i]);
                }
            }
            return string.Join("", charTab);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string str = "some text 124 to change";
            Console.WriteLine(StringExtended.ChangeCaseEverySecond(str));
        }
    }
}
