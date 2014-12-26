namespace UnitySuiceCommons.EventDispatcher.Unity.UnityEvent
{
    /// <summary>
    /// Unity's Monobehaviour#OnApplicationFocus event.
    /// 
    /// @author DisTurBinG
    /// </summary>
    public struct UnityApplicationFocusEvent : IDispatchedEvent
    {
        public readonly bool IsFocused;

        public UnityApplicationFocusEvent(bool isFocused)
        {
            IsFocused = isFocused;
        }
    }
}
