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
            int blocks = (((minutes / blockMinutes + 1) * blockMinutes) + int.Parse(ConfigurationManager.AppSettings["BufferMinutes"]));
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
            var beginDayHour = int.Parse(ConfigurationManager.AppSettings["BeginDayHour"]);
            var beginDayMinute = int.Parse(ConfigurationManager.AppSettings["BeginDayMinute"]);
            var lunchTimeHour = int.Parse(ConfigurationManager.AppSettings["LunchTimeHour"]);
            var lunchTimeMinute = int.Parse(ConfigurationManager.AppSettings["LunchTimeMinute"]);
            var coffeeBreakHour = int.Parse(ConfigurationManager.AppSettings["CoffeeBreakHour"]);
            var coffeeBreakMinute = int.Parse(ConfigurationManager.AppSettings["CoffeeBreakMinute"]);
            var endDayHour = int.Parse(ConfigurationManager.AppSettings["EndDayHour"]);
            var endDayMinute = int.Parse(ConfigurationManager.AppSettings["EndDayMinute"]);

            
            var today = DateTime.Today;
            DateTime itemStartTime = new DateTime(today.Year, today.Month, today.Day, beginDayHour, beginDayMinute, 0);
            DateTime lunchStartTime = new DateTime(today.Year, today.Month, today.Day, lunchTimeHour, lunchTimeMinute, 0);
            DateTime coffeeBreakStartTime = new DateTime(today.Year, today.Month, today.Day, coffeeBreakHour, coffeeBreakMinute, 0);
            DateTime endDayStartTime = new DateTime(today.Year, today.Month, today.Day, endDayHour, endDayMinute, 0);
            List<DayPlanItem> dayPlan = new List<DayPlanItem>();

            bool lunchBreakAdded = false;
            bool coffeeBreakAdded = false;
            foreach (var task in taskList)
            {
                var nextItemStartTime = itemStartTime.AddMinutes(task.Minutes);
                if (itemStartTime >= lunchStartTime && lunchBreakAdded == false)
                {
                    dayPlan.Add(new DayPlanItem() { Name = "Lunch Time!", Time = itemStartTime });
                    itemStartTime = itemStartTime.AddMinutes(60);
                    lunchBreakAdded = true;
                }
                else if (itemStartTime >= coffeeBreakStartTime && coffeeBreakAdded == false)
                {
                    dayPlan.Add(new DayPlanItem() { Name = "Coffee Break!", Time = itemStartTime });
                    itemStartTime = itemStartTime.AddMinutes(15);
                    coffeeBreakAdded = true;

                }
                else if (itemStartTime >= endDayStartTime)
                {
                    dayPlan.Add(new DayPlanItem() { Name = "Stop working for the day!", Time = itemStartTime });
                    return dayPlan;
                }

                dayPlan.Add(new DayPlanItem() { Name = task.Name, Time = itemStartTime });
                itemStartTime = nextItemStartTime;
            }

            return dayPlan;
        }

           
    }
}

