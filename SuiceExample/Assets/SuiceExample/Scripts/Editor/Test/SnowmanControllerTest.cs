using NSubstitute;
using NUnit.Framework;
using SuiceExample.Factory;
using SuiceExample.Snowman;
using UnityEngine;

namespace SuiceExample.Test
{
    /// <summary>
    /// This example test does not cover all test cases due to limitations of testing monobehaviours.
    /// I would need to create an interface that override all Monobehaviour functionality in unity.  Maybe I'll do this next time :).
    /// 
    /// @author DisTurBinG
    /// </summary>
    [TestFixture]
    public class SnowmanControllerTest
    {
        private ISnowmanMoveComponent snowmanMoveComponent = Substitute.For<ISnowmanMoveComponent>();
        private ISnowmanFactory snowmanFactory = Substitute.For<ISnowmanFactory>();

        private SnowmanController snowmanController;

        [SetUp]
        public void Setup()
        {
            snowmanMoveComponent = Substitute.For<ISnowmanMoveComponent>();
            snowmanFactory = Substitute.For<ISnowmanFactory>();
        
            snowmanController = new SnowmanController(snowmanFactory, snowmanMoveComponent);
        }

        [Test]
        public void DestroyWhileMovingTest()
        {
            snowmanMoveComponent.IsMoving().Returns(true);
            Assert.Throws<SnowmanController.CannotDestroyWhileMovingException>(() => 
                snowmanController.Destroy());
        }

        [Test]
        public void SuccessfulDestroyTest()
        {
            snowmanMoveComponent.IsMoving().Returns(false);

            Assert.DoesNotThrow(() => snowmanController.Destroy());
            snowmanFactory.Received(1).ReturnToPool(snowmanController);
        }

        [Test]
        public void TestSnowmanMovement()
        {
            snowmanController.MoveToRandomPosition(5);

            snowmanMoveComponent.Received(1).WalkToPosition(Arg.Is<Vector3>(targetPosition => 
                targetPosition.x >= SnowmanController.MIN_TARGET_MOVE_RANGE.x && targetPosition.x <= SnowmanController.MAX_TARGET_MOVE_RANGE.x &&
                targetPosition.x >= SnowmanController.MIN_TARGET_MOVE_RANGE.y && targetPosition.x <= SnowmanController.MAX_TARGET_MOVE_RANGE.y &&
                targetPosition.x >= SnowmanController.MIN_TARGET_MOVE_RANGE.z && targetPosition.x <= SnowmanController.MAX_TARGET_MOVE_RANGE.z), 5);
        }
    }
}