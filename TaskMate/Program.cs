using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Spectre.Console;

namespace TaskMate
{
    class Program
    { 
        static void Main(string[] args)
        {
                              
            SpectreUI.WelcomeScreen();
                      
        }

        class ReadText
        {
            public static void Read()
            {
                

              AnsiConsole.Markup(File.ReadAllText ("WriteText.txt"));
            }
        }
    }
}