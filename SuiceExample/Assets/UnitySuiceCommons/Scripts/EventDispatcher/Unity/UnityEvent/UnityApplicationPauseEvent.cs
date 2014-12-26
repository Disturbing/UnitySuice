namespace UnitySuiceCommons.EventDispatcher.Unity.UnityEvent
{
    /// <summary>
    /// Unity's Monobehaviour#OnApplicationPause event.
    /// 
    /// @author DisTurBinG
    /// </summary>
    public struct UnityApplicationPauseEvent : IDispatchedEvent
    {
        public readonly bool IsPaused;

        public UnityApplicationPauseEvent(bool isPaused)
        {
            IsPaused = isPaused;
        }
    }
}
