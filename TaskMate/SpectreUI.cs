using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Spectre.Console;
using static System.Collections.Specialized.BitVector32;

namespace TaskMate
{
    public class SpectreUI
    {
        public static void UiDisp()
        {
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Please Make a Selection?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                    .AddChoices(new[] {"New Task", "Edit Tasks", "Display Tasks", "Delete Tasks", "Quit",})
            );

            switch (selection)
            {
                case "New Task":
                    AddTaskMenu();
                    break;
                case "Display Tasks":
                    DisplayTasks();
                    break;
                case "Edit Tasks":
                    SelectEditTask();
                    break;
                case "Delete Tasks":
                    SelectDeleteTask();
                    break;
                case "Quit":
                    return;                 
            }

            UiDisp();
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

        public static void DisplayTasks()
        {
            foreach (var task in Repo.Load()) PrintTask(task);
            static void PrintTask(Task task)
            {
                AnsiConsole.WriteLine($"Name: {task.Name}\t Blocks: {task.Blocks}\t Type: {task.Type}");
            }
        }


        public static void SelectEditTask()
        {
            List<string> nameList = new List<string>();
            //foreach (var task in Repo.Load()) nameList.Add(task.Name);

            foreach (var (task, index) in Repo.Load().WithIndex())
            {
                nameList.Add(index + ". " + task.Name);
            }
            string [] nameArray = nameList.ToArray();
            var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                    .Title("Select a task to edit:")
                    .PageSize(10)
                    .AddChoices(nameArray)
                    .AddChoices(new[] {"", "<- BACK"}));
            //.MoreChoicesText("[grey](Move up and down to reveal more tasks)[/]")

            var taskList = Repo.Load();
            int found = selection.IndexOf(".");
            int length = selection.Length;
            int id = int.Parse(selection.Remove(found, length - found));
            
            SelectEditProperty(id);

        }

        public static void SelectEditProperty(int id)
        {
            Task task = Repo.Load()[id];
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What do you want to edit?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                    .AddChoices(new[] { task.Name, task.Blocks, task.Type, "<- Back", })
                );
            
            if (selection == "<- Back") return;
            else if (selection == task.Name)
            {
                task.Name = AnsiConsole.Ask<string>("Task [green]name[/]?");
                Repo.UpdateTask(task, id);
            }
            else if (selection == task.Blocks)
            {
                task.Blocks = AnsiConsole.Ask<string>("Task [green]blocks[/]?");
                Repo.UpdateTask(task, id);
            }
            else if (selection == task.Name)
            {
                task.Type = AnsiConsole.Ask<string>("Task [green]type[/]?");
                Repo.UpdateTask(task, id);
            }          
        }
        public static void SelectDeleteTask()
        {
            List<string> nameList = new List<string>();
            //foreach (var task in Repo.Load()) nameList.Add(task.Name);

            foreach (var (task, index) in Repo.Load().WithIndex())
            {
                nameList.Add(index + ". " + task.Name);
            }
            string[] nameArray = nameList.ToArray();
            var selectionList = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
                    .Title("Select a task to edit:")
                    .PageSize(10)
                    .AddChoices(nameArray)
                    .AddChoices(new[] { "", "<- BACK" }));
            //.MoreChoicesText("[grey](Move up and down to reveal more tasks)[/]")

            //var taskList = Repo.Load();
            //int found = selection.IndexOf(".");
            //int length = selection.Length;
            //int id = int.Parse(selection.Remove(found, length - found));

            //SelectEditProperty(id);

            foreach (var item in selectionList)
            {
                int found = item.IndexOf(".");
                int length = item.Length;
                int id = int.Parse(item.Remove(found, length - found));
                Repo.DeleteTask(id);
            }
        }
    }
}
