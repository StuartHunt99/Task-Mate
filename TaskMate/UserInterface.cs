using System;
using System.Collections.Generic;

namespace TaskMate
{
	public  class UserInterface
	{
        public static void MenuDisp(List<Task> taskList)
        {
            string option =  "";
            //while (option  != "4" ){

                Console.Clear();
                Console.WriteLine("Select an Option:");
                Console.WriteLine("1: Add Task");
                Console.WriteLine("2: Output Task List");
                Console.WriteLine("3: Save to JSON");
                Console.WriteLine("4: Quit");

                option = Console.ReadLine();
                if (option == "1")
                {
                    Console.WriteLine("1 Selected");
                    AddTask();

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
                    Console.WriteLine("3 Selected");
                }
                else
                {
                    Console.WriteLine("Invalid Selection");
                }
                Console.ReadLine();

            //}
        }

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

    }
}

