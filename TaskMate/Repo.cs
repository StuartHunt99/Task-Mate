using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TaskMate
{
	public class Repo
	{

        public static List<Task> Load()
        {
            string fileName = "TaskLists.json";
            string jsonStringRead = File.ReadAllText(fileName);
            List<Task> tasksReturn = JsonSerializer.Deserialize<List<Task>>(jsonStringRead)!;
            return tasksReturn;
        }



    }
}

