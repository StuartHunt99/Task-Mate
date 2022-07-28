using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using Spectre.Console;
using static System.Collections.Specialized.BitVector32;

namespace TaskMate
{
    public class SpectreUI
    {
        public static void WelcomeScreen()
        {
            AnsiConsole.Clear();

            AnsiConsole.Write(
            new FigletText("TaskMate")
            .LeftAligned()
            .Color(Color.Red));

            var content = new Markup(Repo.WelcomeText()).Centered();

            AnsiConsole.Write(
                new Panel(
                    new Panel(content)
                        .Border(BoxBorder.Rounded)
                        .Collapse()
                        .Header("[yellow]Welcome to TaskMate![/]")
                        .HeaderAlignment(Justify.Center)
                        ));

            Console.ReadKey();

            MainMenu();
        }

        public static void MainMenu()
        {
            AnsiConsole.Clear();

            AnsiConsole.Write(
            new FigletText("TaskMate")
            .LeftAligned()
            .Color(Color.Red));

            var highlightStyle = new Style().Foreground(Color.Lime);
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Please Make a Selection[/]")
                    .PageSize(10)
                    .HighlightStyle(highlightStyle)
                    .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                    .AddChoices(new[] { "Update & Display Schedule", "Display Quadrant", "Display Task List", "Add/Edit/Delete Tasks", "[blue]QUIT[/]" })
            );

            switch (selection)
            {
                case "Update & Display Schedule":
                    DisplayDayPlan();
                    break;
                case "Display Quadrant":
                    DisplayTaskQuadrant();
                    break;
                case "Display Task List":
                    DisplayTasksTable();
                    break;
                case "Add/Edit/Delete Tasks":
                    TaskMenu();
                    break;
                case "[blue]QUIT[/]":
                    return;
            }
            
            MainMenu();
        }

        public static void TaskMenu()
        {
            AnsiConsole.Clear();

            AnsiConsole.Write(
           new FigletText("TaskMate")
           .LeftAligned()
           .Color(Color.Red));

            var highlightStyle = new Style().Foreground(Color.Lime);
            var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[yellow]Please Make a Selection[/]")
                    .PageSize(10)
                    .HighlightStyle(highlightStyle)
                    //.MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                    .AddChoices(new[] { "Display Tasks", "New Task", "Edit Tasks", "Delete Tasks", "[blue]<- RETURN TO MAIN MENU[/]"})
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
                case "[blue]<- RETURN TO MAIN MENU[/]":
                    return;                 
            }

            TaskMenu();
        }


        public static void AddTaskMenu()
        {
            bool intFlag = false;
            int minutes =  0;
            int importance = 0;
            int urgency = 0;

            //AnsiConsole.Clear();
            AnsiConsole.Markup("[yellow]Task Name?[/] ");
            String name = Console.ReadLine();
            
            while (intFlag == false)
            {
                AnsiConsole.Markup("[yellow]How many minutes will this take?[/] ");
                String min = Console.ReadLine();
                if (!int.TryParse(min, out minutes))
                {
                    AnsiConsole.MarkupLine("[red]Please enter a valid number.[/] ");
                }
                else intFlag = true;
            }



            intFlag = false;
            while (intFlag == false)
            {
                AnsiConsole.Markup("[yellow]Level of importance (1-4)? [/]");
                String imp = Console.ReadLine();
                if (!int.TryParse(imp, out importance))
                {
                    AnsiConsole.MarkupLine("[red]Please enter a valid number.[/]");
                    intFlag = false;
                }
                else if (!(importance > 0 && importance < 5))
                {
                    AnsiConsole.MarkupLine("[red]Please enter a valid number.[/]");
                    intFlag = false;
                }
                else intFlag = true;
            }
                       
            intFlag = false;
            while (intFlag == false)
            {
                AnsiConsole.Markup("[yellow]Level of urgency (1-4)?[/] ");
                String imp = Console.ReadLine();
                if (!int.TryParse(imp, out urgency))
                {
                    AnsiConsole.MarkupLine("[red]Please enter a valid number.[/]");
                    intFlag = false;
                }
                else if (!(urgency > 0 && urgency < 5))
                {
                    AnsiConsole.MarkupLine("[red]Please enter a valid number.[/]");
                    intFlag = false;
                }
                else intFlag = true;
            }
            
            var task = new Task()
            {
                Name = name,
                Minutes = minutes,
                Importance = importance,
                Urgency = urgency
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
            table.AddColumn("Importance");
            table.AddColumn("Urgency");

            foreach (Task t in Repo.Load())
            {
                var name = t.Name;
                var minutes = t.Minutes.ToString();
                var importance = t.Importance.ToString();
                var urgency = t.Urgency.ToString();
                table.AddRow(name, minutes, importance, urgency);
            }           
            AnsiConsole.Write(table);
            DisplayKeyPressReturnPrompt();
        
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
                    .Title("[yellow]What do you want to edit?[/]")
                    .PageSize(10)
                    //.MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                    .AddChoices(new[] { $"[blue]NAME:[/] \t {task.Name}", $"[blue]MINUTES:[/] \t {task.Minutes.ToString()}", $"[blue]IMPORTANT:[/] \t {task.Importance.ToString()}", $"[blue]URGENT:[/] \t {task.Urgency.ToString()}", "[blue]<- Back[/]" })
                );
            
            if (selection.Contains("Back")) return;

            else if (selection.Contains(task.Name))
            {
                task.Name = AnsiConsole.Ask<string>("Task [blue]name[/]?");
                Repo.UpdateTask(task, id);
            }

            else if (selection.Contains(task.Minutes.ToString()))
            {
                
                bool intFlag = false;
                while (intFlag == false)
                {
                    
                    
                   string min = AnsiConsole.Ask<string>("Task [blue]minutes[/]?");
                    if (!int.TryParse(min, out int minutes))
                    {
                        AnsiConsole.MarkupLine("[red]Please enter a valid number.[/]");
                    }

                    else
                    {
                        intFlag = true;
                        task.Minutes = minutes;
                        Repo.UpdateTask(task, id);
                    }
                }
            }

            else if (selection.Contains("IMPORTANT"))
            {
                //s = AnsiConsole.Ask<string>("Task [blue]importance[/]?");
                //task.Importance = int.Parse(s);

                bool intFlag = false;
                while (intFlag == false)
                {
                    AnsiConsole.WriteLine("Level of importance (1-4)?");
                    String imp = Console.ReadLine();
                    if (!int.TryParse(imp, out int importance))
                    {
                        AnsiConsole.MarkupLine("[red]Please enter a valid number.[/]");
                        intFlag = false;
                    }
                    else if (!(importance > 0 && importance < 5))
                    {
                        AnsiConsole.MarkupLine("[red]Please enter a valid number.[/]");
                        intFlag = false;
                    }
                    else
                    {
                        task.Importance = importance;
                        intFlag = true;
                    }
                }
                Repo.UpdateTask(task, id);
            }
            
            else if (selection.Contains("URGENT"))
            {
                //s = AnsiConsole.Ask<string>("Task [blue]importance[/]?");
                //task.Importance = int.Parse(s);

                bool intFlag = false;
                while (intFlag == false)
                {
                    AnsiConsole.WriteLine("Level of Urgency (1-4)?");
                    String imp = Console.ReadLine();
                    if (!int.TryParse(imp, out int urgency))
                    {
                        AnsiConsole.MarkupLine("[red]Please enter a valid number.[/]");
                        intFlag = false;
                    }
                    else if (!(urgency > 0 && urgency < 5))
                    {
                        AnsiConsole.MarkupLine("[red]Please enter a valid number.[/]");
                        intFlag = false;
                    }
                    else
                    {
                        task.Urgency = urgency;
                        intFlag = true;
                    }
                }
                Repo.UpdateTask(task, id);               
            }
            SelectEditProperty(id);
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
                    .Title("[yellow]Select all Tasks to [red]DELETE[/], then press ENTER.[/]")
                    .PageSize(10)
                    .AddChoices(nameArray)
                    .AddChoices(new[] { "", "<- BACK" }));
            //.MoreChoicesText("[grey](Move up and down to reveal more tasks)[/]")

            if (!AnsiConsole.Confirm($"[yellow]Are you sure you want to [red]permanently delete[/] {selectionList.Count} tasks?[/]"))
            {                
                return;
            }

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
        }

        public static void DisplaySchedule(List<Task> schedule)
        {
            var table = new Table();
            table.Border = TableBorder.Simple;

            table.AddColumn("Name");
            table.AddColumn("Blocks");
            table.AddColumn("Importance");
            table.AddColumn("Urgency");

            foreach (Task t in Repo.Load())
            {
                var name = t.Name;
                var minutes = t.Minutes.ToString();
                var importance = t.Importance.ToString();
                var urgency = t.Urgency.ToString();
                table.AddRow(name, minutes, importance, urgency);
            }
            AnsiConsole.Write(table);
        }
        
        public static void DisplayTaskQuadrant()
        {
            List<Task> schedule = TaskListUtils.CreateBlockedSchedule();
            string[,] array = new string[4, 4]
            {
                {"","","","" },
                {"","","","" },
                {"","","","" },
                {"","","","" },
            };

            var table = new Table();
            table.Border = TableBorder.Simple;

            table.AddColumn("1");
            table.AddColumn("2");
            table.AddColumn("3");
            table.AddColumn("4");
            for (int i = 0; i < 4; i++)
            {
                table.Columns[i].Width(15);
            }

            foreach (Task t in schedule)
            {
                array[t.Importance - 1, t.Urgency - 1] += (t.Name + Environment.NewLine);
            }

            for (int c = 0; c < 4; c++)
            {
                table.AddRow(array[c, 0], array[c, 1], array[c, 2], array[c, 3]);
            }

            //AnsiConsole.Clear();
            AnsiConsole.Write(table);

            var grid2 = new Grid()
            .AddColumn(new GridColumn().NoWrap().PadRight(4))
            .AddColumn()
            .AddRow("Test", "test")
            .AddRow("Test", "test")
            .AddRow("Test", "test")
            .AddRow("Test", "test");

            var grid = new Grid()
            .AddColumn(new GridColumn().NoWrap().PadRight(4))
            .AddColumn()
            .AddRow(grid2, grid2)
            .AddRow("Test", "test")
            .AddRow("Test", "test")
            .AddRow("Test", "test");



            //AnsiConsole.Write(
            //    new Panel(grid1)
            //        .Header("Information"));

            DisplayKeyPressReturnPrompt();

        }
        
        public static void DisplayDayPlan()
        {
            List<Task> schedule = TaskListUtils.CreateBlockedSchedule();
            List<DayPlanItem> dayPlan = TaskListUtils.CreateDayPlan(schedule);
            foreach (var dayPlanItem in dayPlan)
            {
                if (dayPlanItem.Name.Contains("Lunch Time!") || dayPlanItem.Name.Contains("Break")  || dayPlanItem.Name.Contains("Stop working"))
                {
                    AnsiConsole.Markup($"[lime]{dayPlanItem.Time.ToString("t")} \t {dayPlanItem.Name}[/]");

                }
                else
                {
                    AnsiConsole.Write($"{dayPlanItem.Time.ToString("t")} \t {dayPlanItem.Name}");
                }
                    Console.WriteLine(" ");
            }
            DisplayKeyPressReturnPrompt();
        }

        private static void DisplayKeyPressReturnPrompt()
        {
            Console.WriteLine("");
            AnsiConsole.Markup("[yellow]Press a Key to Return to Main Menu[/]");
            Console.ReadKey();
            AnsiConsole.Clear();
        }
    }
}
