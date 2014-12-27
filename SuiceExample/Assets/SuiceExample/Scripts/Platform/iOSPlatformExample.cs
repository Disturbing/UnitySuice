using UnityEngine;

namespace SuiceExample.Platform
{
#if UNITY_IOS
    /// <summary>
    /// iOS platform example implementation
    /// 
    /// @author DisTurBinG
    /// </summary>
    public class iOSPlatformExample : IPlatformExample
    {
        public void RunPlatformTask()
        {
            Debug.Log("Running platform specific task for DefaultPlatformExample!");
        }
    }
#endif
}
