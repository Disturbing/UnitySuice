using UnityEngine;

namespace SuiceExample.Platform
{
    /// <summary>
    /// DefaultPlatform example implementation
    /// 
    /// @author DisTurBinG
    /// </summary>
    public class DefaultPlatformExample : IPlatformExample
    {
        public void RunPlatformTask()
        {
            Debug.Log("Running platform specific task for DefaultPlatformExample!");
        }
    }
}
