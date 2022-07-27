using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TaskMate
{
    public class TaskListUtils
    {
                
        public static int ConvertToBlocks(int minutes)
        {
            var blockMinutes = int.Parse(ConfigurationManager.AppSettings["BlockMinutes"]);
            int blocks = ((minutes / blockMinutes + 1) * blockMinutes);
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

