using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TaskMate.UnitTests
{
    [TestClass]
    public class RepoTests
    {
        [TestMethod]
        public void Load_RepoInit_IsNotNull()
        {
            //Arrange

            var taskList = Repo.Load();

            //Act

            var initName = taskList[0].Name;

            //Assert

            Assert.AreEqual(initName, "Call Mom");
        }
    }

    [TestClass]
    public class TaskListUtilsTests
    {
        [TestMethod]
        public void CreateScheduleTest()
        {
            //Arrange

            var schedule = TaskListUtils.CreateBlockedSchedule();


            //Act

            var result = schedule;

            //Assert
            int oldUrgency = 4;
            foreach (var task in schedule)
            {

                Assert.IsTrue(task.Urgency <= oldUrgency);
                System.Console.WriteLine($"Name: {task.Name}, Blocks: {task.Minutes}, Priority: {task.Importance}, Urgency: {task.Urgency}");
                oldUrgency = task.Urgency;
            }

            SpectreUI.DisplayTaskQuadrant(schedule);
            
        }
    }
}

