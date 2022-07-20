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

            Assert.AreEqual(initName, "INIT_Name");
        }
    }
}

