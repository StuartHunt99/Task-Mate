using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TaskMate
{
	public class Task
	{
		public string Name { get; set; }
		public string Blocks { get; set; }
		public string Type { get; set; }

   

    public static Task AddTask()
        {

             
	        Console.Clear();
            Console.WriteLine("Task Name?");
            String name = Console.ReadLine();
            Console.WriteLine("Task Time Blocks?");
            String blocks = Console.ReadLine();
            Console.WriteLine("Task Type?");
            String type = Console.ReadLine();

            var task = new Task()
            {
                Name = name,
                Blocks = blocks,
                Type = type
            };

            return task;
        }
        //static List<Task> InitTaskList()
        //   {

        //       var tasks = new List<Task>();

        //       tasks.Add(new Task()
        //       {
        //           Name = "email",
        //           Blocks = 2,
        //           Type = "ToDo"
        //       });

        //       tasks.Add(new Task()
        //       {
        //           Name = "call",
        //           Blocks = 2,
        //           Type = "ToDo"
        //       });

        //       tasks.Add(new Task()
        //       {
        //           Name = "work",
        //           Blocks = 1,
        //           Type = "ToDo"
        //       });

        //       tasks.Add(new Task()
        //       {
        //           Name = "cook",
        //           Blocks = 1,
        //           Type = "ToDo"
        //       });

        //       return tasks;
        //}



    }


}


