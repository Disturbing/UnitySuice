using System.Collections.Generic;
using DTools.Suice;
using SuiceExample.Snowman;
using UnityEngine;
using UnitySuiceCommons.Resource;

namespace SuiceExample.Factory
{
    /// <summary>
    /// Very simple example of loading a pooled controller - this one isn't as effecient because of the mass instantiations at the beginning.
    /// To maximize effeciency - have the factory atually load a pooled object, which contains all pooled instances at the beginning.
    /// 
    /// In the provide function - it's also simplified.  This is where you would want to customize anything with particular params, reset - etc.
    /// 
    /// @author DisTurBinG
    /// </summary>
    [Singleton]
    public class SnowmanFactory : Factory<ISnowmanController>, IInitializable, ISnowmanFactory
    {
        public const string SNOWMAN_POOL_CONTAINER_NAME = "_SnowmanPool";
        public const int INITIAL_POOL_COUNT = 10;
        public const string SNOWMAN_ASSET_PATH = "Snowman";

        private readonly IUnityResources unityResources;

        private readonly Queue<ISnowmanController> snowmanPool = new Queue<ISnowmanController>();
        private GameObject prefabTemplate;
        private GameObject poolContainer;

        [Inject]
        public SnowmanFactory(IUnityResources unityResources)
        {
            this.unityResources = unityResources;
        }

        public void Initialize()
        {
            poolContainer = unityResources.CreateEmptyGameObject(SNOWMAN_POOL_CONTAINER_NAME);
            prefabTemplate = (GameObject)unityResources.Load(SNOWMAN_ASSET_PATH, typeof(GameObject));

            for (int i=0; i < INITIAL_POOL_COUNT; i++) {
                snowmanPool.Enqueue(CreateSnowmanMoveController());
            }
        }

        private SnowmanController CreateSnowmanMoveController()
        {
            GameObject gameObject = (GameObject)unityResources.Instantiate(prefabTemplate);

            gameObject.name = prefabTemplate.name;
            gameObject.transform.parent = poolContainer.transform;
            gameObject.SetActive(false);

            return new SnowmanController(this, gameObject.GetComponent<SnowmanMoveComponent>());
        }

        public override ISnowmanController Provide()
        {
            ISnowmanController snowmanController = snowmanPool.Count > 0 
                ? snowmanPool.Dequeue()
                : CreateSnowmanMoveController();

            snowmanController.SetParentTransform(null);
            snowmanController.SetActive(true);

            return snowmanController;
        }

        public void ReturnToPool(ISnowmanController controller)
        {
            // Reset controller to default values
            controller.SetPosition(prefabTemplate.transform.position);
            controller.SetActive(false);
            controller.SetParentTransform(poolContainer.transform);

            snowmanPool.Enqueue(controller);
        }
    }
}
