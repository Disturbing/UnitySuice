using System;
using NSubstitute;
using NUnit.Framework;
using SuiceExample.Task;
using UnitySuiceCommons.TaskManager;

namespace SuiceExample.Test
{
    [TestFixture]
    public class TaskExampleTest
    {
        private TaskExample taskExample;
        private IUnityTaskManager taskManager;

        [SetUp]
        public void Setup()
        {
            taskManager = Substitute.For<IUnityTaskManager>();
            taskExample = new TaskExample(taskManager);
        }

        [Test]
        public void InitTest()
        {
            taskExample.Initialize();
            taskManager.Received(1).AddTask(Arg.Any<Action>(), TimeSpan.FromSeconds(5).TotalMilliseconds);
        }
    }
}