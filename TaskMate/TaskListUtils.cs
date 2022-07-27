using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMate
{
    public class TaskListUtils
    {
        
        //public static void EditTask2(string selection)
        //{
        //    var taskList = Repo.Load();
        //    int found = selection.IndexOf(".");
        //    int length = selection.Length;
        //    int id = int.Parse(selection.Remove(found, length - found));
        //    Console.WriteLine(taskList[id].Name);


        //    Console.ReadLine();
        //}


        public static int ConvertToBlocks(int minutes)
        {
            int blocks = ((minutes / 25 + 1) * 25);
            return blocks;
        }

        public static List<Task> CreateBlockedSchedule()
        {
           
            List<Task> taskList = Repo.Load();
            List<Task> orderedTaskList = taskList.OrderByDescending(x => x.Urgency).ThenByDescending(x => x.Importance).ToList();
            

            foreach (var task in orderedTaskList)
            {
                task.Minutes = ConvertToBlocks(task.Minutes);                         
            }
            
            return orderedTaskList;
        }

        public static List<DayPlanItem> CreateDayPlan(List<Task> taskList)
        {
            List<DayPlanItem> dayPlan = new List<DayPlanItem>();
            DateTime planItemStartTime = new DateTime(2010, 1, 1, 8, 0, 0);
            foreach (var task in taskList)
            {
                dayPlan.Add(new DayPlanItem() { Name = task.Name, Time = planItemStartTime });
                planItemStartTime = planItemStartTime.AddMinutes(task.Minutes);
            }

            return dayPlan;
        }

           
    }
}

