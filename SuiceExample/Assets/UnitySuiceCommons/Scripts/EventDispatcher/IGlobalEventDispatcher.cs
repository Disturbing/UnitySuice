namespace UnitySuiceCommons.EventDispatcher
{
    /// <summary>
    /// @author DisTurBinG
    /// </summary>
    public interface IGlobalEventDispatcher
    {
        void BroadcastEvent(IDispatchedEvent ev);
        void RegisterListener(object listener);
        void RemoveListener(object listener);
    }
}