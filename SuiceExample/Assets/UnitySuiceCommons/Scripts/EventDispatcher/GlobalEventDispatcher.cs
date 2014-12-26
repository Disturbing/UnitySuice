namespace UnitySuiceCommons.EventDispatcher
{
    /// <summary>
    /// Wrapper for broadcasting global events
    /// 
    /// @author Joseph Cooper
    /// </summary>
    public class GlobalEventDispatcher : IGlobalEventDispatcher
    {
        private readonly EventDispatcher eventDispatcher = new EventDispatcher();

        public void BroadcastEvent(IDispatchedEvent ev)
        {
            eventDispatcher.BroadcastEvent(ev);
        }

        public void RegisterListener(object listener)
        {
            eventDispatcher.AddEventListener(listener);
        }

        public void RemoveListener(object listener)
        {
            eventDispatcher.RemoveEventListener(listener);
        }
    }
}
