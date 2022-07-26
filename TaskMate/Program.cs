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


            //SpectreUI.TaskMenu();
            // List<Task> schedule = Repo.Load(); 
            //SpectreUI.DisplayTaskQuadrant(schedule);

            //TaskListUtils.CreateSchedule();

            SpectreUI.MainMenu();
            
        }
    }
}