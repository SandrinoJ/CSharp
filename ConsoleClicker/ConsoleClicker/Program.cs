﻿using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;



namespace ConsoleClicker
{

    class Program
    {
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        static void Main(string[] args)
        {
            int powerx = 1, count1 = 0;
            float price1f = 50f, price2f = 100f, bars = 0f, totalBars = 0f, menuSelection = 0, timedx = 0f;
            bool isSecret = false;
            Console.TreatControlCAsInput = true;
            Console.Title = "Console Clicker, Made by Renekris";
            Console.WriteLine("Welcome to Console Clicker - WIP\n\n");
            Console.ReadKey();
        menu:
            Console.Clear();
            Console.WriteLine("~~~Welcome~~~");
            Console.WriteLine("<- / -> Arrows to Collect Bars \nESC / M - Market\n");
            if (isSecret == true)
            {
                Console.WriteLine("S - Secret menu");
            }
            
            ConsoleKey press = Console.ReadKey(true).Key;
            switch (press)
            {
                case ConsoleKey.LeftArrow:
                case ConsoleKey.RightArrow:
                    menuSelection = 1;
                    break;
                case ConsoleKey.Escape:
                case ConsoleKey.M:
                    menuSelection = 2;
                    break;
                case ConsoleKey.S:
                    if (isSecret == true)
                    {
                        menuSelection = 3;
                    }
                    else
                    {
                        goto menu;
                    }
                    break;
                default:
                    goto menu;
            }
            //Bars
            while (menuSelection == 1)
            {
            
                Console.Clear();
                Console.WriteLine("Press <- and -> arrows to earn Bars\nPress ESC to go back to the menu.\n");
            bars:
                Console.WriteLine("~{0:n2} Bars", bars);

                press = Console.ReadKey(true).Key;
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                ClearCurrentConsoleLine();
                
                    
                
                //Bar Values
                switch (press)
                {
                    case ConsoleKey.Escape:
                        goto menu;
                    case ConsoleKey.LeftArrow:
                        if (press == ConsoleKey.LeftArrow && count1 == 0)
                        {
                            bars += 1 * powerx;
                            totalBars += 1 * powerx;
                            count1 = 1;
                            Thread.Sleep(10);
                            goto bars;
                        }
                        else
                            goto bars;
                    case ConsoleKey.RightArrow:
                        if (press == ConsoleKey.RightArrow && count1 == 1)
                        {
                            bars += 1 * powerx;
                            totalBars += 1 * powerx;
                            count1 = 0;
                            Thread.Sleep(10);
                            goto bars;
                        }
                        else
                            goto bars;
                    default:
                        goto bars;
                }
            }
            //Market
            while (menuSelection == 2)
            {
                store:
                Console.Clear();
                Console.WriteLine("~~Welcome to the store~~\nYou have about ~{0:n2} bars.\nESC to menu\nEnter a number to buy\n", bars);
                Console.WriteLine("1.\t+= 1x enter power\n\t~{0:n2} Bars\n\n", price1f);
                Console.WriteLine("2.\tSecret\n\t~{0:n2} Bars\n\n", price2f);
                press = Console.ReadKey(true).Key;
                switch (press)
                {
                    //Power upgrade
                    case ConsoleKey.D1:
                        if (bars >= price1f)
                        {
                            bars = bars - price1f;
                            price1f = price1f * 1.21f;
                            powerx += 1;
                            Console.Clear();
                            Console.WriteLine("You have just bought += 1x power\n~{0:n2} Bars remaining.\n\nESC to go back.", bars);
                            Console.ReadKey();
                            goto store;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("You don't have enough Bars!");
                            Console.ReadKey();
                            goto store;
                        }
                    //Secret upgrade
                    case ConsoleKey.D2:
                        if (bars >= price2f && isSecret == false)
                        {
                            Console.Clear();
                            isSecret = true;
                            bars = bars - price2f;
                            Console.WriteLine("You have just bought a secret.\n~{0:n2} Bars remaining.\n\nESC to go back.", bars);
                            Console.ReadKey();
                            goto store;
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("You don't have enough Bars!");
                            Console.ReadKey();
                            goto store;
                        } 
                    case ConsoleKey.Escape:
                        goto menu;
                    default:
                        goto store;
                }
            }
            //Secret menu
            while (menuSelection == 3)
            {
                Console.Clear();
                Console.WriteLine("This is the secret menu\nThis is where your statistics go.\n");
                Console.WriteLine("Current bars: {0:n2}", bars);
                Console.WriteLine("Total bars: {0:n2}\n", totalBars);
                Console.WriteLine("Any key to go back.");
                Console.ReadKey();
                goto menu;
            }
        }
    }
}

