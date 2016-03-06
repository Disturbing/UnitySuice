using DTools.Suice;
using Mono.CompilerServices.SymbolWriter;
using NSubstitute;
using NUnit.Framework;
using SuiceExample.Factory;
using SuiceExample.Snowman;
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
        private SnowmanPoolManager snowmanPoolManager;
        private GameObject prefabTemplate;
        private GameObject poolContainer;
        private IProvider<ISnowmanController> snowmanControllerProvider;
        
        [SetUp]
        public void Setup()
        {
            // TODO: Remove this as it won't be needed when game object wrapper exists!
            prefabTemplate = new GameObject("Snowman", typeof (SnowmanMoveComponent));
            poolContainer = new GameObject();

            unityResources = Substitute.For<IUnityResources>();
            snowmanControllerProvider = Substitute.For<IProvider<ISnowmanController>>();

            unityResources.CreateEmptyGameObject(SnowmanPoolManager.SNOWMAN_POOL_CONTAINER_NAME).Returns(poolContainer);
            unityResources.Load(SnowmanController.SNOWMAN_ASSET_PATH, typeof(GameObject)).Returns(prefabTemplate);
            unityResources.Instantiate(prefabTemplate).Returns(prefabTemplate);
            snowmanControllerProvider.Provide().Returns(Substitute.For<ISnowmanController>());
            
            snowmanPoolManager = new SnowmanPoolManager(unityResources, snowmanControllerProvider);
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
            snowmanPoolManager.Initialize();

            unityResources.Received(1).CreateEmptyGameObject(SnowmanPoolManager.SNOWMAN_POOL_CONTAINER_NAME);
            snowmanControllerProvider.Received(SnowmanPoolManager.INITIAL_POOL_COUNT).Provide();
        }

        [Test]
        public void TestPooledProvide()
        {
            snowmanPoolManager.Initialize();

            for (int i = 0; i < SnowmanPoolManager.INITIAL_POOL_COUNT; i++) {
                snowmanPoolManager.Provide();
            }

            snowmanControllerProvider.Received(SnowmanPoolManager.INITIAL_POOL_COUNT).Provide();
        }

        [Test]
        public void TestMoreThanInitialPoolProvide()
        {
            int snowmenToCreate = SnowmanPoolManager.INITIAL_POOL_COUNT + 5;
            snowmanPoolManager.Initialize();

            snowmanControllerProvider.Received(SnowmanPoolManager.INITIAL_POOL_COUNT).Provide();

            for (int i = 0; i < snowmenToCreate; i++) {
                snowmanPoolManager.Provide();
            }

            snowmanControllerProvider.Received(snowmenToCreate).Provide();
        }
    }
}