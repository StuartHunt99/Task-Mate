using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TaskMate
{
	public class Task
	{
		public string Name { get; set; }
		public int Blocks { get; set; }
		public string Type { get; set; }

     public void PrintTasks()
        {
			Console.WriteLine($"Name: {Name}, Blocks {Blocks}, Type{Type}");

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


