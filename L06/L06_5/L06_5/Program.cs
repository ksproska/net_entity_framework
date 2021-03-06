/*
5.  Napisać metodę DrawCard „rysującą” wizytówkę. Metoda otrzymuje w parametrach pierwszą linię wizytówki, drugą linię, znak obramowania, szerokość obramowania, 
minimalną szerokość całej wizytówki. Oprócz pierwszego parametru pozostałe są opcjonalne (wybrać własne domyślne wartości). Zademonstrować różne wywołania 
metody w tym z użyciem parametrów nazwanych. Wizytówka to prostokątna ramka z wycentrowanymi napisami np. dla zestawu danych DrawCard („Ryszard”,”Rys”,”X”,2,20) „narysuje”: 

XXXXXXXXXXXXXXXXXXXX 
XXXXXXXXXXXXXXXXXXXX 
XX     Ryszard    XX 
XX       Rys      XX 
XXXXXXXXXXXXXXXXXXXX 
XXXXXXXXXXXXXXXXXXXX 
 */
using System;

namespace L06_5
{
    class Program
    {   static void DrawCard(string name, string surname="Kowalski", char filler='X', int frameWidth=2, int frameMinWidth=20)
        {   
            // adjusting vals
            int numberNameLInes = 2;
            if(frameWidth < 0)
            {
                frameWidth = 2;
            }
            int cardWidth = frameMinWidth;
            int extraFill = 2;
            int tempNameLen = name.Length + extraFill + 2 * frameWidth;
            if (tempNameLen > cardWidth)
            {
                cardWidth = tempNameLen;
            }
            int tempSurnameLen = surname.Length + extraFill + 2 * frameWidth;
            if (tempSurnameLen > cardWidth)
            {
                cardWidth = tempSurnameLen;
            }
            // creating lines
            char[][] allStrings = new char[numberNameLInes + 2 * frameWidth][];

            char[] frame = new char[cardWidth];
            Array.Fill(frame, filler);

            char[] l1 = new char[cardWidth];
            char[] l2 = new char[cardWidth];
            Array.Fill(l1, ' ');
            Array.Fill(l2, ' ');

            allStrings[frameWidth] = l1;
            allStrings[frameWidth + 1] = l2;

            // filling with frame
            for (int i = 0; i < frameWidth; i++)
            {
                allStrings[i] = frame;
                allStrings[allStrings.Length - 1 - i] = frame;

                l1[i] = filler;
                l1[cardWidth - 1 - i] = filler;
                l2[i] = filler;
                l2[cardWidth - 1 - i] = filler;
            }

            // filling with name
            (int startImie, int startNazwisko) indexes = (
                (int)(cardWidth / 2 - name.Length / 2), 
                (int)(cardWidth / 2 - surname.Length / 2));
            
            for(int i=0; i<name.Length; i++)
            {
                l1[i + indexes.startImie] = name[i];
            }
            for (int i = 0; i < surname.Length; i++)
            {
                l2[i + indexes.startNazwisko] = surname[i];
            }

            // printing whole thing
            foreach (char[] line in allStrings)
            {
                Console.WriteLine(String.Join("", line));
            }
        }
        static void Main(string[] args)
        {
            DrawCard("Ryszard");
            Console.WriteLine("");
            DrawCard("Kamila", filler: '0', frameWidth: 3);
            Console.WriteLine("");
            DrawCard("Kamila", frameMinWidth: 5, frameWidth: 1);
            Console.WriteLine("");
        }
    }
}
