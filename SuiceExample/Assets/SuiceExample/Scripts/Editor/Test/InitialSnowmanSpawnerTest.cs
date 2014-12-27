using System;
using NSubstitute;
using NUnit.Framework;
using SuiceExample.Snowman;
using UnitySuiceCommons.EventDispatcher.Unity.UnityEvent;
using UnitySuiceCommons.TaskManager;

namespace SuiceExample.Test
{
    [TestFixture]
    public class InitialSnowmanSpawnerTest
    {
        private ISnowmanController snowman1;
        private ISnowmanController snowman2;
        private ISnowmanRandomSpawner snowmanRandomSpawner;
        private IUnityTaskManager taskManager;

        private InitialSnowmanSpawner initialSpawner;

        [SetUp]
        public void Setup()
        {
            snowman1 = Substitute.For<ISnowmanController>();
            snowman2 = Substitute.For<ISnowmanController>();
            snowmanRandomSpawner = Substitute.For<ISnowmanRandomSpawner>();
            taskManager = Substitute.For<IUnityTaskManager>();

            initialSpawner = new InitialSnowmanSpawner(snowman1, snowman2, taskManager, snowmanRandomSpawner);
        }

        [Test]
        public void TestStart()
        {
            initialSpawner.OnStart(new UnityStartEvent());

            snowman1.Received(1).MoveToRandomPosition(InitialSnowmanSpawner.SNOWMAN_MOVE_TIME_LENGTH);
            snowman2.Received(1).MoveToRandomPosition(InitialSnowmanSpawner.SNOWMAN_MOVE_TIME_LENGTH);
            taskManager.Received(1).AddTask(snowmanRandomSpawner.Start,
                TimeSpan.FromSeconds(InitialSnowmanSpawner.SNOWMAN_MOVE_TIME_LENGTH).TotalMilliseconds);
        }
    }
}
