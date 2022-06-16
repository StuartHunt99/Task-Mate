using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TaskMate
{
    class Program
    {
        static void Main(string[] args)
        {

            //var tasksReturn = ReadTaskList();
            var tasksReturn = ReadThis();

            //DISPLAY ALL THE TASKS IN THE LIST
	        foreach (var task in tasksReturn) Console.WriteLine(task.Name);

            //TEST THE ABILITY TO CALL PROPETIES FROM ONE PARTICULAR LIST ITEM
            Console.WriteLine(tasksReturn[0].Name);

            string jsonString = JsonSerializer.Serialize(tasksReturn);
            //File.WriteAllText("TaskLists.json", jsonString);

            static List<Task> ReadThis()
            {
                string fileName = "TaskLists.json";
                string jsonStringRead = File.ReadAllText(fileName);
                List<Task> tasksReturn = JsonSerializer.Deserialize<List<Task>>(jsonStringRead)!;
                return tasksReturn;
            }

            //BUILT AN INTIAL LIST OF TASKS
            //static List<Task> InitTaskList()
            //{

            //    var tasks = new List<Task>();

            //    tasks.Add(new Task()
            //    {
            //        Name = "email",
            //        Blocks = 2,
            //        Type = "ToDo"
            //    });

            //    tasks.Add(new Task()
            //    {
            //        Name = "call",
            //        Blocks = 2,
            //        Type = "ToDo"
            //    });

            //    tasks.Add(new Task()
            //    {
            //        Name = "work",
            //        Blocks = 1,
            //        Type = "ToDo"
            //    });

            //    tasks.Add(new Task()
            //    {
            //        Name = "cook",
            //        Blocks = 1,
            //        Type = "ToDo"
            //    });

            //    return tasks;
            //}
        }
    }
}

