﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Automatic_Youtube_Downloader
{
    class Program
    {
        private static int counter = 0;
        private static string[] sequence = new string[] { "   .   ", "  ...  ", " ..... ", ".. . ..", ".  .  ." };
        private static string[] pythonPackages = new[] { "pytube", "youtube-dl" };
        private static Regex removeNum = new Regex(@"[0-9=./\()*&^%$#@!]+");
        private static List<string> urls = new List<string>();
        private static string[] filesCheck = new[] { "youtube-audio.py", "youtube-video.py", "/Windows/py.exe", "youtube-video-dl.py", "ffmpeg.exe" };
        private static readonly string urlVideoPath = "urlsVideo.txt";
        private static readonly string urlAudioPath = "urlsAudio.txt";

        static public void Turn()
        {
            counter++;

            if (counter >= sequence.Length)
                counter = 0;

            Console.Write(sequence[counter]);
            Console.SetCursorPosition(Console.CursorLeft - sequence[counter].Length, Console.CursorTop);
        }

        static void IsRunning(Process status, bool animation)
        {
            counter = 0;
            while (!status.HasExited)
            {
                if (animation == true)
                {
                    Turn();
                }
                Thread.Sleep(250);
            }
        }

        static void FileCheck()
        {
            foreach (string s in filesCheck)
            {
                if (!File.Exists(s))
                {
                    Console.WriteLine("Puudu on vajalik fail {0}", s);
                    if (s == "/Windows/py.exe")
                    {
                        Console.WriteLine("Installi endale Python 3");
                        Process.Start("https://www.python.org/downloads/");
                    }
                    Console.ReadKey();
                    Environment.Exit(0);
                }
            }
            //Kontrollib kas õiged pythoni osad olemas
            Process pipFreeze = new Process();
            ConsoleOutput(pipFreeze, "py.exe", "-m pip freeze");

            List<string> pipFreezeList = new List<string>();
            pipFreezeList.Add(removeNum.Replace(pipFreeze.StandardOutput.ReadToEnd(), ""));
            foreach (var freeze in pipFreezeList)
            {
                if (!pythonPackages.All(freeze.Contains))
                {
                    Console.Clear();
                    Console.WriteLine("Ühekordne valmistus. Installin [{0}] + [{1}]", pythonPackages[0], pythonPackages[1]);
                    Process pipInstall = new Process();
                    IsRunning(ConsoleOutput(pipInstall, "py.exe", "-m pip install pytube youtube-dl --no-warn-script-location"), true);
                }
            }
        }

        static Process ConsoleOutput(Process process, string fileName, string arguments)
        {
            process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            process.StartInfo.FileName = fileName;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            return process;
        }

        static void UrlRead(string urlPath)
        {
            using (StreamReader reader = new StreamReader(urlPath))
            {
                while (!reader.EndOfStream)
                    urls.Add(reader.ReadLine());
                reader.Close();
            }
        }

        static void Main(string[] args)
        {


            try
            {
                Console.Title = "Automatic Youtube V/A Downloader";
                Console.OutputEncoding = Encoding.Unicode;
                Console.InputEncoding = Encoding.Unicode;
                //Kontrollib failide olemasolu

                FileCheck();
                //Teeb faili kui see puudub
                if (!File.Exists(urlAudioPath))
                    File.Create(urlAudioPath).Dispose();
                if (!File.Exists(urlVideoPath))
                    File.Create(urlVideoPath).Dispose();
                Console.WriteLine("[1]> Video\n" +
                                  "[2]> Audio");
                ConsoleKey press = Console.ReadKey().Key;
                switch (press)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Video();
                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Audio();
                        break;
                }

                Console.WriteLine("Sisesta text faili URL-id reakaupa,\n" +
                                  "Salvesta ja sule fail kui oled valmis allalaadima.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Palun teata veast. renekrispohlak@gmail.com | github.com/renekris");
                Console.ReadKey();
                throw;
            }
        }
        /// <summary>
        /// Audio Failideks
        /// </summary>
        static void Audio()
        {
            IsRunning(Process.Start(urlAudioPath), false);
            //Loeb sisestatud URL'id
            UrlRead(urlAudioPath);

            Console.Clear();
            //Kontrollib korra veel faili olemasolu
            if (File.Exists(urlAudioPath))
            {
                foreach (var VARIABLE in urls)
                {
                    //Laeb videod alla

                    /*
                    parser.add_argument('--url', type=str)
                    parser.add_argument('--noplaylist', type=bool, default=true)
                   */
                    Process videoInstall = new Process();
                    Console.WriteLine("{0} / {1} faili", urls.IndexOf(VARIABLE) + 1, urls.Count);
                    IsRunning(ConsoleOutput(videoInstall, "py.exe", "youtube-audio.py --url " + VARIABLE), true);
                    Console.WriteLine("Writing ended");
                    Console.Clear();
                }
                DialogResult ans = MessageBox.Show("Kas sa soovid vaadata allalaetud faile?", "Kausta avamine", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (ans == DialogResult.Yes)
                    Process.Start("explorer.exe", "/select," + Path.GetFullPath("Automatic Youtube Downloader.exe"));
                else
                    Environment.Exit(0);
            }
            Environment.Exit(0);
        }

        /// <summary>
        /// Video Failideks
        /// </summary>

        static void Video()
        {
            //Ei jätka kuni failid pannakse kinni
            IsRunning(Process.Start(urlVideoPath), false);

            //Loeb sisestatud URL'id
            UrlRead(urlVideoPath);

            Console.Clear();
            //Kontrollib korra veel faili olemasolu
            if (File.Exists(urlVideoPath))
            {
                foreach (var VARIABLE in urls)
                {
                    //Laeb videod alla

                    Process videoInstall = new Process();
                    Console.WriteLine("{0} / {1} faili", urls.IndexOf(VARIABLE) + 1, urls.Count);
                    //Older version:
                    //IsRunning(ConsoleOutput(videoInstall, "py.exe", "youtube-video.py --url " + VARIABLE), true);
                    //Newer version:
                    IsRunning(ConsoleOutput(videoInstall, "py.exe", "youtube-video-dl.py --url " + VARIABLE), true);
                    Console.WriteLine("Writing ended");
                    Console.Clear();
                }
                DialogResult ans = MessageBox.Show("Kas sa soovid vaadata allalaetud faile?", "Kausta avamine", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.DefaultDesktopOnly);
                if (ans == DialogResult.Yes)
                    Process.Start("explorer.exe", "/select," + Path.GetFullPath("Automatic Youtube Downloader.exe"));
                else
                    Environment.Exit(0);
            }
            Environment.Exit(0);
        }
    }
}
