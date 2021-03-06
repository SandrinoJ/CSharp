﻿using System;
using System.Text;

namespace Juku_Koolipäev
{
    class Program
    {
        static void ColorGreen(string inputString)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(inputString);
            Console.ResetColor();
        }

        static void ColorBlue(string inputString)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(inputString);
            Console.ResetColor();
        }

        static void ColorDarkBlue(string inputString)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(inputString);
            Console.ResetColor();
        }

        static void ColorRed(string inputString)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(inputString);
            Console.ResetColor();
        }

        static void JukuValues(float jukuEnergia)
        {
            Console.WriteLine("Juku Energia: {0:P0}", jukuEnergia);
            if (jukuEnergia >= 0.50)
            {
                ColorGreen("Juku on energiline!\n");
            }
            else if (jukuEnergia <= 0.49 && jukuEnergia >= 0.15)
            {
                ColorBlue("Juku on kerges masenduses ja ta tuju on kehv.\n");
            }
            else if (jukuEnergia <= 0.14 && jukuEnergia >= 0.01)
            {
                ColorDarkBlue("Juku on depresiivsuse äärel!\n");
            }
            else if (jukuEnergia < 0.01)
            {
                ColorRed("Jukul on seest valus ja ta on suremas depresiivsusest!\n");
            }
        }

        static void JukuHinne(int hinne)
        {
            if (hinne == 1)
            {
                Console.WriteLine("Juku sai endale hinde X");
            }
            else
            {
                Console.WriteLine("Juku sai endale hinde {0}", hinne);
            }
        }

        static void JukuSwitchCase(ref float jukuEnergia, int hinne)
        {
            switch (hinne)
            {
                case 1:
                    jukuEnergia -= 0.75f;
                    break;
                case 2:
                    jukuEnergia -= 0.50f;
                    break;
                case 3:
                    jukuEnergia -= 0.40f;
                    break;
                case 4:
                    jukuEnergia -= 0.30f;
                    break;
                case 5:
                    jukuEnergia -= 0.20f;
                    break;
                default:
                    jukuEnergia -= 1f;
                    break;
            }

            if (jukuEnergia < 0)
            {
                jukuEnergia = 0;
            }
        }

        static void JukuToiduValik(ref float jukuEnergia, ref string jukuToiduValik)
        {
            anyKey:
            JukuValues(jukuEnergia);
            Console.WriteLine("Neljas tund on söögi aeg, vali jukule, mida ta sööb\n" +
                              "1. Värskekapsa borš sealihaga\n" +
                              "2. Makaronid hakklihaga\n" +
                              "3. Friikartulid viineritega\n" +
                              "4. Köögiviljavormiroog\n" +
                              "5. Praetud kanafilee\n");
            ConsoleKey press = Console.ReadKey(false).Key;
            Console.Clear();
            switch (press)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    jukuToiduValik = "Värskekapsa borš sealihaga";
                    jukuEnergia += 0.20f;
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    jukuToiduValik = "Makaronid hakklihaga";
                    jukuEnergia += 0.25f;
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    jukuToiduValik = "Friikartulid viineritega";
                    jukuEnergia += 0.15f;
                    break;
                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    jukuToiduValik = "Köögiviljavormiroog";
                    jukuEnergia += 0.10f;
                    break;
                case ConsoleKey.NumPad5:
                case ConsoleKey.D5:
                    jukuToiduValik = "Praetud kanafilee";
                    jukuEnergia += 0.50f;
                    break;
                default:
                    goto anyKey;
            }
        }

        static float KooliÕde(ref float jukuEnergia)
        {
            if (jukuEnergia <= 0.01)
            {
                Console.Clear();
                JukuValues(jukuEnergia);
                jukuEnergia += 0.15f;
                Console.WriteLine("Juku on ekstriimselt nõrk ja ta viiakse kooliõe juurde.\n" +
                                  "Kooliõde on heameelne ja annab Jukule energiabatooni.\n" +
                                  "+15% energiat.");
                Console.ReadKey();
                Console.Clear();
            }

            return jukuEnergia;
        }


        static void Main(string[] args)
        {
            Console.Title = "Juku Koolipäev, By Renekris";
            Random rng = new Random();
            Console.OutputEncoding = Encoding.Unicode;
            int sündmus, hinne;
            float jukuEnergia = 1f;
            string[] syndimus = {"tunnikontroll", "kontrolltöö", "kodutööde esitamine"};
            string jukuToiduValik = null;
            ////MENU
            JukuValues(jukuEnergia);
            Console.WriteLine("Sa oled just kooli jõudnud ja sa oled täiesti ärkvel!\n\n" +
                              "Tänane tunniplaan on:\n" +
                              "1. tund | Programmeerimine\n" +
                              "2. tund | Eesti keel\n" +
                              "3. tund | Inglise keel\n" +
                              "3. tund | Lõuna\n" +
                              "4. tund | Keemia\n" +
                              "5. tund | Matemaatika\n" +
                              "6. tund | Operatsiooni süsteemide alused");
            Console.ReadKey();
            Console.Clear();
            ////TUND 1
            //rng
            sündmus = rng.Next(0, 3);
            hinne = rng.Next(1, 6);
            //switch case | decreases the energy
            JukuSwitchCase(ref jukuEnergia, hinne);
            //checkib kas jukuEnergia on < 0.01
            KooliÕde(ref jukuEnergia);
            //juku energia number | juku olukord
            JukuValues(jukuEnergia);
            Console.WriteLine("Esimene tund on Programmeerimine\nÕpetaja ütles Jukule, et täna on {0}",
                syndimus[sündmus]);
            //1-5 hinne jukule
            JukuHinne(hinne);
            Console.ReadKey();
            Console.Clear();
            ////TUND 2
            sündmus = rng.Next(0, 3);
            hinne = rng.Next(1, 6);
            JukuSwitchCase(ref jukuEnergia, hinne);
            KooliÕde(ref jukuEnergia);
            JukuValues(jukuEnergia);
            Console.WriteLine("Teine tund on Eesti keel\nÕpetaja ütles Jukule, et täna on {0}",
                syndimus[sündmus]);
            JukuHinne(hinne);
            Console.ReadKey();
            Console.Clear();
            ////TUND 3
            sündmus = rng.Next(0, 3);
            hinne = rng.Next(1, 6);
            JukuSwitchCase(ref jukuEnergia, hinne);
            KooliÕde(ref jukuEnergia);
            JukuValues(jukuEnergia);
            Console.WriteLine("Kolmas tund on Inglise keel\nÕpetaja ütles Jukule, et täna on {0}",
                syndimus[sündmus]);
            JukuHinne(hinne);
            Console.ReadKey();
            Console.Clear();
            ////LÕUNA 4
            JukuToiduValik(ref jukuEnergia, ref jukuToiduValik);
            JukuValues(jukuEnergia);
            Console.WriteLine("Juku sõi {0} ja tal hakkas kohe parem.", jukuToiduValik.Clone());
            Console.ReadKey();
            Console.Clear();
            ////TUND 5
            sündmus = rng.Next(0, 3);
            hinne = rng.Next(1, 6);
            KooliÕde(ref jukuEnergia);
            JukuSwitchCase(ref jukuEnergia, hinne);
            JukuValues(jukuEnergia);
            Console.WriteLine("Viies tund on Keemia\nÕpetaja ütles Jukule, et täna on {0}", syndimus[sündmus]);
            JukuHinne(hinne);
            Console.ReadKey();
            Console.Clear();
            ////TUND 6
            sündmus = rng.Next(0, 3);
            hinne = rng.Next(1, 6);
            KooliÕde(ref jukuEnergia);
            JukuSwitchCase(ref jukuEnergia, hinne);
            JukuValues(jukuEnergia);
            Console.WriteLine("Kuues tund on Matemaatika\nÕpetaja ütles Jukule, et täna on {0}",
                syndimus[sündmus]);
            JukuHinne(hinne);
            Console.ReadKey();
            Console.Clear();
            ////TUND 7
            sündmus = rng.Next(0, 3);
            hinne = rng.Next(1, 6);
            KooliÕde(ref jukuEnergia);
            JukuSwitchCase(ref jukuEnergia, hinne);
            JukuValues(jukuEnergia);
            Console.WriteLine(
                "Seitsmes tund on Operatsiooni süsteemide alused\nÕpetaja ütles Jukule, et täna on {0}",
                syndimus[sündmus]);
            JukuHinne(hinne);
            Console.ReadKey();
            if (hinne == 5)
            {
                Console.Clear();
                jukuEnergia = 1f;
                JukuValues(jukuEnergia);
                Console.WriteLine("Juku sai viimase tunni hinde {0},\n" +
                                  "ehk Juku saab varem koju + Juku elurõõm on tagasi tulnud", hinne);
                Console.ReadKey();
                Environment.Exit(0);
            }
            else
            {
                Console.Clear();
                KooliÕde(ref jukuEnergia);
                JukuValues(jukuEnergia);
                Console.WriteLine("Juku sai viimase tunni hinde {0},\n" +
                                  "Juku peab kauemaks kooli jääma asju parandama", hinne);
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }
}
