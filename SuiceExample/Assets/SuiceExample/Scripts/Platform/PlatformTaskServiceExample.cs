using DTools.Suice;
using UnitySuiceCommons.EventDispatcher;
using UnitySuiceCommons.EventDispatcher.Unity.UnityEvent;

namespace SuiceExample.Platform
{
    /// <summary>
    /// Example of what it would be like injecting a platform specific dependency.
    /// 
    /// This example would better be used for in app purchases across multiple games, or even communication with native level DLLs.
    /// 
    /// But for the same of time, we will just inject and call a simple debug statement.
    /// 
    /// @author DisTurBinG
    /// </summary>
    [Singleton]
    public class PlatformTaskServiceExample
    {
        private readonly IPlatformExample platformExample;

        [Inject]
        public PlatformTaskServiceExample(IPlatformExample platformExample)
        {
            this.platformExample = platformExample;
        }

        [EventListener]
        public void OnStart(UnityStartEvent startEvent)
        {
            platformExample.RunPlatformTask();
        }
    }
}
