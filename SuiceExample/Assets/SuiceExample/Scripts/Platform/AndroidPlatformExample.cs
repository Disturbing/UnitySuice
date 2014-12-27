using UnityEngine;

namespace SuiceExample.Platform
{
#if UNITY_ANDROID
    /// <summary>
    /// Android platform example implementation
    /// 
    /// @author DisTurBinG
    /// </summary>
    public class AndroidPlatformExample : IPlatformExample
    {
        public void RunPlatformTask()
        {
            Debug.Log("Running platform specific task for Android!");
        }
    }
#endif
}
