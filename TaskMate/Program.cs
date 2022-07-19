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
          
            var taskList = Repo.Load();
            UserInterface.MenuDisp(taskList);


            return;

            //Task tempTask = (taskList[1].AddTask());
            //Console.WriteLine(tempTask.Name);
            //taskList.Add(tempTask);
		
	   

            




            

        }
    }
}


//taskList[1].PrintTasks();

//TEST THE ABILITY TO CALL PROPETIES FROM ONE PARTICULAR LIST ITEM
//Console.WriteLine(taskList[0].Name);

//string jsonString = JsonSerializer.Serialize(taskList);
//File.WriteAllText("TaskLists.json", jsonString);

//foreach (var task in taskList) Console.WriteLine(task.Name);

//var tasksReturn = ReadTaskList();