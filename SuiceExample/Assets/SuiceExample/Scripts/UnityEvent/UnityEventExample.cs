using DTools.Suice;
using UnityEngine;
using UnitySuiceCommons.EventDispatcher;
using UnitySuiceCommons.EventDispatcher.Unity.UnityEvent;

namespace UnitySuiceCommons.UnityEvent
{
    /// <summary>
    /// Example of listening to unity events automatically using Suice!
    /// 
    /// @author DisTurBinG
    /// </summary>
    [Singleton(Scope.EAGER_SINGLETON)]
    public class UnityEventExample : IUnityEventExample
    {
        [EventListener]
        public void OnStart(UnityStartEvent startEvent)
        {
            Debug.Log("Unity Start Event Called!");
        }
    }
}
