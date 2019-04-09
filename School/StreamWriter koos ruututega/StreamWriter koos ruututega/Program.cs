﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamWriter_koos_ruututega
{
    class Program
    {
        static string Size()
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = new FileInfo("data.txt").Length;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return String.Format("{0:0.##} {1}", len, sizes[order]);
        }
            
        static void Main(string[] args)
        {
            bool mainSwitch = false;
            bool mainSwitchCurrent = false;
            /*
             * Tekita programmi abil fail,
             * milles oleksid arvud ja nende ruudud
             * ühest kahekümneni.
             */


            if (!File.Exists("data.txt"))
                File.Create("data.txt").Dispose();
            while (true)
            {
                Console.WriteLine("[1]> Sisesta arv\n" +
                                  "[2]> Lülita otsakirjutamine [{0}]\n" +
                                  "[3]> Ava fail\n" +
                                  "[4]> Ava faili asukoht\n" +
                                  "[5]> Puhasta fail [LINES: {1} & SIZE: {2}]", mainSwitchCurrent.ToString().ToUpper(), File.ReadLines(@"data.txt").Count(), Size());
                ConsoleKey press = Console.ReadKey().Key;
                switch (press)
                {
                    case ConsoleKey.D1:
                        Arv(mainSwitchCurrent);
                        break;
                    case ConsoleKey.D2:
                        mainSwitchCurrent = Overwrite(ref mainSwitch);
                        break;
                    case ConsoleKey.D3:
                        Process.Start("data.txt");
                        break;
                    case ConsoleKey.D4:
                        Process.Start("explorer.exe", "/select, " + "data.txt");
                        break;
                    case ConsoleKey.D5:
                        File.WriteAllText("data.txt", String.Empty);
                        break;
                }
                Console.Clear();
            }
        }
        static bool Overwrite(ref bool mainSwitch)
        {
            if (mainSwitch)
            {
                mainSwitch = false;
                return false;
            }
            else
            {
                mainSwitch = true;
                return true;
            }
        }

        static void Arv(bool mainSwitchCurrent)
        {
            Console.Clear();
            Console.Write("Sisesta mitmendani:");
            int kordus = int.Parse(Console.ReadLine().Trim());
            using (StreamWriter writer = new StreamWriter(@"data.txt", mainSwitchCurrent))
            {
                for (int i = 0; i < kordus; i++)
                {
                    writer.WriteLine("{0, 6:G6}^2 = {1, 6:G10}", i + 1, Math.Pow(i + 1, 2));
                }
                writer.Close();
            }

            using (StreamReader reader = new StreamReader(@"data.txt"))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    Console.WriteLine(line);
                    line = reader.ReadLine();
                }
                reader.Close();
            }
            Console.ReadKey();
        }
    }
}