using UnitySuiceCommons.EventDispatcher.Unity.UnityEvent;

namespace UnitySuiceCommons.EventDispatcher.Unity
{
    /// <summary>
    /// Broadcasts Events related to the UnityEngine.
    /// 
    /// @author DisTurBinG
    /// </summary>
    public class UnityEventDispatcher
    {
        private readonly EventDispatcher eventDispatcher = new EventDispatcher();

        // Cached updates for performance
        private readonly UnityUpdateEvent unityUpdateEvent = new UnityUpdateEvent();
        private readonly UnityLateUpdateEvent unityLateUpdate = new UnityLateUpdateEvent();
        private readonly UnityFixedUpdateEvent unityFixedUpdateEvent = new UnityFixedUpdateEvent();

        public void RegisterEventListener(object eventListener)
        {
            eventDispatcher.AddEventListener(eventListener);
        }

        public void BroadcastStartEvent()
        {
            eventDispatcher.BroadcastEvent(new UnityStartEvent());
        }

        public void BroadcastUpdateEvent()
        {
            eventDispatcher.BroadcastEvent(unityUpdateEvent);
        }

        public void BroadcastLateUpdateEvent()
        {
            eventDispatcher.BroadcastEvent(unityLateUpdate);
        }

        public void BroadcastFixedUpdate()
        {
            eventDispatcher.BroadcastEvent(unityFixedUpdateEvent);
        }

        public void BroadcastOnApplicationPauseEvent(bool isPaused)
        {
            eventDispatcher.BroadcastEvent(new UnityApplicationPauseEvent(isPaused));
        }

        public void BroadcastOnApplicationFocusEvent(bool isFocused)
        {
            eventDispatcher.BroadcastEvent(new UnityApplicationFocusEvent(isFocused));
        }

        public void BroadcastOnApplicationQuitEvent()
        {
            eventDispatcher.BroadcastEvent(new UnityApplicationQuitEvent());
        }
    }
}
