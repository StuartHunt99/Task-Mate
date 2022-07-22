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
                    DisplayTasksTable();
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
            bool intFlag = false;
            int minutes =  0;
            int importance = 0;

            Console.Clear();
            AnsiConsole.WriteLine("Task Name?");
            String name = Console.ReadLine();
            
            while (intFlag == false)
            {
                AnsiConsole.WriteLine("How many minutes will this take?");
                String min = Console.ReadLine();
                if (!int.TryParse(min, out minutes))
                {
                    Console.WriteLine("Please enter a valid number.");                    
                }
                else intFlag = true;
            }



            intFlag = false;
            while (intFlag == false)
            {
                AnsiConsole.WriteLine("Level of importance (1-4)?");
                String imp = Console.ReadLine();
                if (!int.TryParse(imp, out importance))
                {
                    Console.WriteLine("Please enter a valid number.");
                    intFlag = false;
                }
                else if (!(importance > 1 && importance < 4))
                {
                    Console.WriteLine("Please enter a valid number.");
                    intFlag = false;
                }
                else intFlag = true;
            }

            var task = new Task()
            {
                Name = name,
                Minutes = minutes,
                Importance = importance
            };

            Repo.AddTask(task); ;
        }

        public static void DisplayTasks()
        {
            foreach (var task in Repo.Load()) PrintTask(task);
            static void PrintTask(Task task)
            {
                AnsiConsole.WriteLine($"Name: {task.Name}\t Minutes: {task.Minutes}\t Type: {task.Importance}");
            }
        }

        public static void DisplayTasksTable()
        {
            var table = new Table();
            table.Border = TableBorder.Simple;

            table.AddColumn("Name");
            table.AddColumn("Minutes");
            table.AddColumn("Type");
                      
            foreach (Task t in Repo.Load())
            {
                var name = t.Name;
                var minutes = t.Minutes.ToString();
                var importance = t.Importance.ToString();
                table.AddRow(name, minutes, importance);
            }           
            AnsiConsole.Write(table);
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

            if (selection != "<- BACK" && selection != "")
            {
                var taskList = Repo.Load();
                int found = selection.IndexOf(".");
                int length = selection.Length;
                int id = int.Parse(selection.Remove(found, length - found));

                SelectEditProperty(id);
            }
        }

        public static void SelectEditProperty(int id)
        {
            Task task = Repo.Load()[id];
            string s = string.Empty;
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What do you want to edit?")
                    .PageSize(10)
                    .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                    .AddChoices(new[] { task.Name, task.Minutes.ToString(), task.Importance.ToString(), "<- Back", })
                );
            
            if (selection == "<- Back") return;
            else if (selection == task.Name)
            {
                task.Name = AnsiConsole.Ask<string>("Task [green]name[/]?");
                Repo.UpdateTask(task, id);
            }
            else if (selection == task.Minutes.ToString())
            {
                s = AnsiConsole.Ask<string>("Task [green]minutes[/]?");
                task.Minutes = int.Parse(s);
                Repo.UpdateTask(task, id);
            }
            else if (selection == task.Importance.ToString())
            {
                s = AnsiConsole.Ask<string>("Task [green]importance[/]?");
                task.Importance = int.Parse(s);
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
            //var selectionListR = selectionList.Reverse;
            for (int i = selectionList.Count - 1; i >= 0; i--)
            {
                int found = selectionList[i].IndexOf(".");
                if (found != -1)
                {
                    int length = selectionList[i].Length;
                    int id = int.Parse(selectionList[i].Remove(found, length - found));
                    Repo.DeleteTask(id);
                }


            }
            //foreach (string item in selectionList)
            //{
            //    int found = item.IndexOf(".");
            //    if (found != -1)
            //        {
            //        int length = item.Length;
            //        int id = int.Parse(item.Remove(found, length - found));
            //        Repo.DeleteTask(id);
            //        }
            //}

        }
    }
}
