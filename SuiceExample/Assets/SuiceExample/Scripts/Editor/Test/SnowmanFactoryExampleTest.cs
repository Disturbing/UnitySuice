using Mono.CompilerServices.SymbolWriter;
using NSubstitute;
using NUnit.Framework;
using SuiceExample.Factory;
using UnityEngine;
using UnitySuiceCommons.Resource;

namespace SuiceExample.Test
{
    /// <summary>
    /// All test cases are not covered again because Unity Resources expose GameObject which is not testable.
    /// I would need to create a Wrapper GameObject class in order to test this.
    /// 
    /// @author DisTurBinG
    /// </summary>
    [TestFixture]
    public class SnowmanFactoryExampleTest
    {
        private IUnityResources unityResources;
        private SnowmanFactory snowmanFactory;
        private GameObject prefabTemplate;
        private GameObject poolContainer;

        [SetUp]
        public void Setup()
        {
            // TODO: Remove this as it won't be needed when game object wrapper exists!
            prefabTemplate = new GameObject("Snowman", typeof (SnowmanMoveComponent));
            poolContainer = new GameObject();

            unityResources = Substitute.For<IUnityResources>();
            unityResources.CreateEmptyGameObject(SnowmanFactory.SNOWMAN_POOL_CONTAINER_NAME).Returns(poolContainer);
            unityResources.Load(SnowmanFactory.SNOWMAN_ASSET_PATH, typeof (GameObject)).Returns(prefabTemplate);
            unityResources.Instantiate(prefabTemplate).Returns(prefabTemplate);


            //TODO: End Remove

            snowmanFactory = new SnowmanFactory(unityResources);
        }

        [TearDown]
        public void Cleanup()
        {
            Object.DestroyImmediate(prefabTemplate);
            Object.DestroyImmediate(poolContainer);
        }

        [Test]
        public void TestInit()
        {
            snowmanFactory.Initialize();

            unityResources.Received(1).CreateEmptyGameObject(SnowmanFactory.SNOWMAN_POOL_CONTAINER_NAME);
            unityResources.Received(SnowmanFactory.INITIAL_POOL_COUNT).Instantiate(Arg.Any<GameObject>());
        }

        [Test]
        public void TestPooledProvide()
        {
            snowmanFactory.Initialize();

            for (int i = 0; i < SnowmanFactory.INITIAL_POOL_COUNT; i++) {
                snowmanFactory.Provide();
            }

            unityResources.Received(SnowmanFactory.INITIAL_POOL_COUNT).Instantiate(Arg.Any<GameObject>());
        }

        [Test]
        public void TestMoreThanInitialPoolProvide()
        {
            int snowmenToCreate = SnowmanFactory.INITIAL_POOL_COUNT + 5;
            snowmanFactory.Initialize();

            unityResources.Received(SnowmanFactory.INITIAL_POOL_COUNT).Instantiate(Arg.Any<GameObject>());

            for (int i = 0; i < snowmenToCreate; i++) {
                snowmanFactory.Provide();
            }

            unityResources.Received(snowmenToCreate).Instantiate(Arg.Any<GameObject>());
        }
    }
}