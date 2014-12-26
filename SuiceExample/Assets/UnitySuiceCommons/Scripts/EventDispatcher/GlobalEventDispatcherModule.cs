
using DTools.Suice;

namespace UnitySuiceCommons.EventDispatcher
{
    /// <summary>
    /// Module which registers global event dispatcher to Suice system.
    /// 
    /// @author DisTurBInG
    /// </summary>
    public class GlobalEventDispatcherModule : AbstractModule
    {
        internal readonly GlobalEventDispatcher GlobalEventDispatcher = new GlobalEventDispatcher();

        public override void Configure()
        {
            Bind<IGlobalEventDispatcher>().ToInstance(GlobalEventDispatcher);
        }
    }
}
