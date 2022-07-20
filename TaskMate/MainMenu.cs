using System;
using System.Collections.Generic;

namespace TaskMate
{
	public  class UserInterface
	{
        public static void MenuDisp(List<Task> taskList)
        {
            Task tempTask = new Task();
            string option =  "";
            while (option  != "4"){

                Console.WriteLine("Select an Option:");
                Console.WriteLine("1: Add Task");
                Console.WriteLine("2: Output Task List");
                Console.WriteLine("3: Save to JSON");
                Console.WriteLine("4: Quit");

                option = Console.ReadLine();
                if (option == "1")
                {
                    Console.WriteLine("1 Selected");
                    taskList.Add(Task.AddTask());

                }
                else if (option == "2")
                {
                    Console.WriteLine("2 Selected");
                    foreach (var task in taskList) PrintTasks(task);

                    static void PrintTasks(Task task)
                    {
                        Console.WriteLine($"Name: {task.Name}\t Blocks: {task.Blocks}\t Type: {task.Type}");
                    }
                }
                else if (option == "3")
                {
                    Console.WriteLine("Saving!");
                }
                else if (option == "4")
                {
                    Console.WriteLine("Goodbye");
                        }
                else
                {
                    Console.WriteLine("Invalid Selection");
                }

            }
        }

        

    }
}

