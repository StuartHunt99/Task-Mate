using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spectre.Console;

namespace TaskMate
{
    public class SpectreUI
    {
        //List<Task> taskList = Repo.Load();

        public static void UiDisp()
        {

            // Ask for the user's favorite fruit
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Please Make a Selection?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                    .AddChoices(new[] {
            "New Task", "Edit Tasks", "Display Tasks",
            "Quit",
                    }));


            switch (selection)
            {
                case "New Task":
                    AddTaskMenu();
                    break;

                case "Display Tasks":
                    PrintTasks();
                    break;

                case "Edit Tasks":
                    EditTask();
                    break;

                case "Quit":
                    return;
                 

            }

            UiDisp();
        }


        public static void TaskTableDisp()
        {

        }


        public static void AddTaskMenu()
        {
            Console.Clear();
            AnsiConsole.WriteLine("Task Name?");
            String name = Console.ReadLine();
            AnsiConsole.WriteLine("Task Time Blocks?");
            String blocks = Console.ReadLine();
            AnsiConsole.WriteLine("Task Type?");
            String type = Console.ReadLine();

            var task = new Task()
            {
                Name = name,
                Blocks = blocks,
                Type = type
            };

            Repo.AddTask(task); ;
        }

        public static void PrintTasks()
        {
            foreach (var task in Repo.Load()) PrintTask(task);
            static void PrintTask(Task task)
            {
                Console.WriteLine($"Name: {task.Name}\t Blocks: {task.Blocks}\t Type: {task.Type}");
            }
        }


        public static void EditTask()
        {
            List<string> nameList = new List<string>();
            foreach (var task in Repo.Load()) nameList.Add(task.Name);
           string [] nameArray = nameList.ToArray();
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select a task to edit:")
                    .PageSize(10)
                    //.MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                    .AddChoices(nameArray)
                    .AddChoices(new[] {"", "<- BACK"})
                    );


        }

        //MULTISELECTION VERSION OF EDITTASK()

        //public static void EditTask()
        //{
        //    var nameList = new List<string>();

        //    //string[] taskNames = taskList.Name.ToArray();

        //    foreach (var task in Repo.Load()) nameList.Add(task.Name);
        //    string [] nameArray = nameList.ToArray();

        //    var selection = AnsiConsole.Prompt(
        //        new MultiSelectionPrompt<string>()
        //            .Title("Select a Task to edit")
        //            .NotRequired() 
        //            .PageSize(10)

        //            .MoreChoicesText("[grey](Move up and down to see more tasks)[/]")
        //            .InstructionsText(
        //                "[grey](Press [blue]<space>[/] to toggle a task, " +
        //                "[green]<enter>[/] to accept)[/]")
        //            .AddChoices(nameArray ));


        //Choice Group Example for Future Reference

        //.AddChoiceGroup("Berries", new string[]
        //                {
        //                    "Blueberries", "Blackberries", "Strawberries", "Boysenberries", "Raspberries"

        //                })


        // Write the selected fruits to the terminal

    }
    
    
}
