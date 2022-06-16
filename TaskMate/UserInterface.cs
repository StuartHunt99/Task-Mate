using System;
namespace TaskMate
{
	public class UserInterface
	{
		public void MenuDisp()
		{
		}

		public void AddTask()
		{
			Console.Clear();
			Console.WriteLine("Task Name?");
            String Name  = Console.ReadLine();
            Console.WriteLine("Task Time Blocks?");
			String Blocks = Console.ReadLine();
			Console.WriteLine("Task Type?");
			String Type = Console.ReadLine();

		}
	}
}

