using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TaskMate
{
    public class TaskListUtils
    {

        public static void EditTask2(string selection)
        {
            var taskList = Repo.Load();
            int found = selection.IndexOf(".");
            int length = selection.Length;
            int id = int.Parse(selection.Remove(found, length - found));
            Console.WriteLine(taskList[id].Name);


            Console.ReadLine();
        }
    }
}

