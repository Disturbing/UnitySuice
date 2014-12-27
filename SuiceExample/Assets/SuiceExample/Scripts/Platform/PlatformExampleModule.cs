using DTools.Suice;

namespace SuiceExample.Platform
{
    public class PlatformExampleModule : AbstractModule
    {
        public override void Configure()
        {
#if UNITY_IOS
            Bind<IPlatformExample>().To<iOSPlatformExample>().In(Scope.SINGLETON);

#elif UNITY_ANDROID
            Bind<IPlatformExample>().To<AndroidPlatformExample>().In(Scope.SINGLETON);

#else
            Bind<IPlatformExample>().To<DefaultPlatformExample>().In(Scope.SINGLETON);
#endif
        }
    }
}
