﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Elutee
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Random rng = new Random();
            int teenimineMin = 0, teenimineMax = 0;
            int majandusCount = 0, majandusKriis = 0;
            const int aastamax = 42;
            int aastaCurrent = 0;
            int kõrvalepanekCurrent = 0;
            Console.Title = "Kuidas rikkaks saada";
            Console.WriteLine("Kuidas rikkaks saada!\nAlustan 17 aastasena");
            Console.ReadKey();
            Console.Clear();
            for (int i = 0; i <= aastamax; i++)
            {
                Console.Clear();
                aastaCurrent++;
                Console.WriteLine("Praegune eluaasta: {0} | Kõrvalolev raha: €{1:N0}\n", aastaCurrent + 17, RahaGain(aastaCurrent, ref kõrvalepanekCurrent, teenimineMax, teenimineMin));
                EventCourse(i, ref majandusKriis, majandusCount, ref kõrvalepanekCurrent, ref teenimineMin, ref teenimineMax);
                if (kõrvalepanekCurrent > 1000000)
                {
                    break;
                }
                Console.ReadKey();
            }

            Console.WriteLine("Oled teeninud > €1,000,000" +
                              "\nLähed nüüd pensionile.");
            Console.ReadKey();
        }

        static int RahaGain(int aastaCurrent, ref int kõrvalepanekCurrent, int teenimineMax, int teenimineMin)
        {
            if (aastaCurrent > 1)
            {
                Random rng = new Random();
                int kõrvalepanek = rng.Next(teenimineMin, teenimineMax);
                return kõrvalepanekCurrent += (kõrvalepanek * 12) - 500; 
            }

            return 0;
        }

        static void EventCourse(int i, ref int majandusCount, int majandusKriis, ref int kõrvalepanekCurrent, ref int teenimineMin, ref int teenimineMax)
        {
            Random rng = new Random();
            string temp = "";
            string[] töödStrings = new[] { "Andmeturbeinspektori", "C# programmeerija", "Andmetungia", "IT spetsialisti", "Tarkvaraarendaja" };
            string[] keelStrings = new[] { "Python", "C++", "Javascript", "Java", "PHP" };
            MajandusEvent(i, majandusKriis, majandusCount, ref kõrvalepanekCurrent);
            switch (i + 17)
            {
                case 17:
                    temp = töödStrings[rng.Next(0, 5)];
                    Console.WriteLine("Oled kooli lõpetanud ja valid tööd. Said omale {0} töö", temp);
                    break;
                case 30:
                    temp = keelStrings[rng.Next(0, 5)];
                    Console.WriteLine("Lähed kooli edasi õppima, valisid {0} keele", temp);
                    break;
                case 32:
                    temp = töödStrings[rng.Next(0, 5)];
                    Console.WriteLine("Oled selgeks õppinud veel ühe programmeerimise keele ja valid uue töö. Sa valisid {0}", temp);

                    break;
                default:
                    Console.WriteLine("~");
                    break;
            }
            switch (temp)
            {
                case "Andmeturbeinspektori":
                    teenimineMin = 1400;
                    teenimineMax = 1681;
                    break;
                case "C# programmeerija":
                    teenimineMin = 1900;
                    teenimineMax = 3600;
                    break;
                case "Andmetungia":
                    teenimineMin = 2100;
                    teenimineMax = 2400;
                    break;
                case "IT spetsialisti":
                    teenimineMin = 1900;
                    teenimineMax = 3600;
                    break;
                case "Tarkvaraarendaja":
                    teenimineMin = 1900;
                    teenimineMax = 2300;
                    break;
            }

        }

        static void MajandusEvent(int i, int majandusKriis, int majandusCount, ref int kõrvalepanekCurrent)
        {
            Random rng = new Random();
            int addNext = rng.Next(50000, 100000);
            if (i + 17 > 39 && i + 17 < 43)
            {
                Console.WriteLine("Majandus kriis!\n+ {0}", addNext);
                kõrvalepanekCurrent += addNext;
            }
        }
    }
}
