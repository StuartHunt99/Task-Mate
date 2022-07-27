using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TaskMate
{
	public class Repo
	{

        public static string jsonStringRead = File.ReadAllText("TaskLists.json");

        public static List<Task> Load()
        {
            
            jsonStringRead = File.ReadAllText("TaskLists.json");
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

