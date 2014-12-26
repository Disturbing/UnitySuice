using UnitySuiceCommons.EventDispatcher.Unity;
using UnityEngine;

namespace CmnEventDispatcher.Util
{
    /// <summary>
    /// Wrapper for UnityEventDispatcher to work with Unity's MonoBehavior methods.
    /// 
    /// @author Joseph Cooper
    /// </summary>
    public class UnityEventDispatcherComponent : MonoBehaviour
    {
        private readonly UnityEventDispatcher unityEventDispatcher = new UnityEventDispatcher();

        public void RegisterEventListener(object eventListener)
        {
            unityEventDispatcher.RegisterEventListener(eventListener);
        }

        void Start()
        {
            unityEventDispatcher.BroadcastStartEvent();
        }

        void Update()
        {
            unityEventDispatcher.BroadcastUpdateEvent();                
        }

        void LateUpdate()
        {
            unityEventDispatcher.BroadcastLateUpdateEvent();                
        }

        void FixedUpdate()
        {
            unityEventDispatcher.BroadcastFixedUpdate();
        }

        void OnApplicationPause(bool isPaused)
        {
            unityEventDispatcher.BroadcastOnApplicationPauseEvent(isPaused);
        }

        void OnApplicationFocus(bool isFocused)
        {
            unityEventDispatcher.BroadcastOnApplicationFocusEvent(isFocused);
        }

        void OnApplicationQuit()
        {
            unityEventDispatcher.BroadcastOnApplicationQuitEvent();
        }
    }
}
