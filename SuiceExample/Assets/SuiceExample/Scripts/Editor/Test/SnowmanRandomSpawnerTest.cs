using System;
using NSubstitute;
using NUnit.Framework;
using SuiceExample.Snowman;
using UnitySuiceCommons.TaskManager;

namespace SuiceExample.Test
{
    [TestFixture]
    public class SnowmanRandomSpawnerTest
    {
        private ISnowmanFactory snowmanFactory;
        private IUnityTaskManager taskManager;
        private SnowmanRandomSpawner randomSpawner;

        [SetUp]
        public void Setup()
        {
            snowmanFactory = Substitute.For<ISnowmanFactory>();
            taskManager = Substitute.For<IUnityTaskManager>();

            randomSpawner = new SnowmanRandomSpawner(snowmanFactory, taskManager);
        }

        [Test]
        public void TestSingleStart()
        {
            Assert.DoesNotThrow(() => randomSpawner.Start());

            taskManager.Received(1).AddRepetableTask(Arg.Any<Func<bool>>(),
                TimeSpan.FromSeconds(SnowmanRandomSpawner.MOVEMENT_TIME_LENGTH_S).TotalMilliseconds);
        }

        [Test]
        public void TestDoubleStart()
        {
            Assert.DoesNotThrow(() => randomSpawner.Start());
            Assert.Throws<SnowmanRandomSpawner.AlreadyStartedException>(() => randomSpawner.Start());
        }
    }
}
