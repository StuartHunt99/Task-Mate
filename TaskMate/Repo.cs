using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TaskMate
{
	public class Repo
	{


        //public static string jsonStringRead = File.ReadAllText("TaskLists.json");

        public static void InitializeTaskList()
        {
            List<Task> initList = new List<Task>();

            initList.Add(new Task() { Name = "Send Email", Minutes = 18, Importance = 2, Urgency = 2 });
            initList.Add(new Task() { Name = "Call Mom", Minutes = 20, Importance = 1, Urgency = 4 });
            initList.Add(new Task() { Name = "Finish Code", Minutes = 90, Importance = 4, Urgency = 3 });
            initList.Add(new Task() { Name = "Fix Bugs", Minutes = 65, Importance = 3, Urgency = 2 });
            initList.Add(new Task() { Name = "Walk the Dog", Minutes = 15, Importance = 3, Urgency = 3 });
            initList.Add(new Task() { Name = "Write Code", Minutes = 45, Importance = 3, Urgency = 1 });
            initList.Add(new Task() { Name = "Learn new code", Minutes = 60, Importance = 4, Urgency = 1 });
            initList.Add(new Task() { Name = "Write report for boss", Minutes = 55, Importance = 4, Urgency = 4 });
            initList.Add(new Task() { Name = "Organize Comic Books", Minutes = 45, Importance = 2, Urgency = 1 });
            initList.Add(new Task() { Name = "Count Paperclips", Minutes = 30, Importance = 1, Urgency = 1 });
         
            string jsonString = JsonSerializer.Serialize(initList);
            File.WriteAllText("TaskLists.json", jsonString);
        }

        public static string WelcomeText()
        {
            string welcomeText = "\n\nTaskMate is designed to help plan your day as efficiently \n" +
                "and productively as possible. \n\n" +
                "" +
                "[Lime]How does it work?[/]\n" +
                "After entering your list of tasks that you'd like to accomplish \n" +
                "for the day, your tasks are sorted according to the productivity \n" +
                "principles of the [red]Eisenhower Quadrant.[/]  \n\n" +
                "" +
                "Next, a schedule is created for your entire work day, organized \n" +
                "in 25-minute time blocks according to the [red]Pomodoro Technique.[/]\n\n" +
                "" +
                "[lime]TaskMate customizes your day, so you can be sure your \n" +
                "time is truly well-spent![/]\n\n\n" +
                "" +
                "" +
                "[yellow]Press ANY KEY to continue...[/]\n";

            return welcomeText;
        }

        public static List<Task> Load()
        {
            
            string jsonStringRead = File.ReadAllText("TaskLists.json");
            List<Task> tasksReturn = JsonSerializer.Deserialize<List<Task>>(jsonStringRead)!;
            return tasksReturn;
        }

        public static void AddTask(Task task)
        {
            var currentList = Repo.Load();
            currentList.Add(task);
            string jsonString = JsonSerializer.Serialize(currentList);
            File.WriteAllText("TaskLists.json", jsonString);
        }

        public static void UpdateTask(Task task, int id)
        {
            var currentList = Repo.Load();
            currentList[id] = task;
            string jsonString = JsonSerializer.Serialize(currentList);
            File.WriteAllText("TaskLists.json", jsonString);
        }

        public static void DeleteTask(int id)
        {
            var currentList = Repo.Load();
            currentList.RemoveAt(id);
            string jsonString = JsonSerializer.Serialize(currentList);
            File.WriteAllText("TaskLists.json", jsonString);

        }
    }
}

