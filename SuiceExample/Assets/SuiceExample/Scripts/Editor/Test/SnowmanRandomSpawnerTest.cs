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
        private ISnowmanPoolManager snowmanPoolManager;
        private IUnityTaskManager taskManager;
        private SnowmanRandomSpawner randomSpawner;

        [SetUp]
        public void Setup()
        {
            snowmanPoolManager = Substitute.For<ISnowmanPoolManager>();
            taskManager = Substitute.For<IUnityTaskManager>();

            randomSpawner = new SnowmanRandomSpawner(snowmanPoolManager, taskManager);
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
