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
		public string Blocks { get; set; }
		public string Type { get; set; }

        public static void EditTask(string name)
        {
            var currentList = Repo.Load();


        }
    }

	

	

}


