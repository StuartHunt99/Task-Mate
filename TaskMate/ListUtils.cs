using System;
using System.Collections.Generic;

namespace TaskMate
{
    public class ListUtils
    {
        public ListUtils()
        {
        }

        public static List<Task> AddTask(List<Task> taskList, Task task)
        { taskList.Add(task);
            return taskList;
        }

    }
}

