using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Spectre.Console;


namespace TaskMate
{
	public class Task
	{
		public string Name { get; set; }
		public int Minutes { get; set; }
		public int Importance { get; set; }
        public int Urgency { get; set; }


        public static void EditTask(string name)
        {
            var currentList = Repo.Load();


        }
    }

	

	

}


