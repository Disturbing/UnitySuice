using DTools.Suice;
using UnityEngine;

namespace UnitySuiceCommons.TaskManager
{
    /// <summary>
    /// Module used to bind task manager to the Injection system
    /// 
    /// @author DisTurBinG
    /// </summary>
    public class UnityTaskManagerModule : AbstractModule
    {
        private readonly GameObject unityTaskManagerObject;

        public UnityTaskManagerModule(GameObject unityTaskManagerObject)
        {
            this.unityTaskManagerObject = unityTaskManagerObject;
        }

        protected override void Configure()
        {
            Bind<IUnityTaskManager>().ToInstance(unityTaskManagerObject.AddComponent<UnityTaskManager>());
        }
    }
}
