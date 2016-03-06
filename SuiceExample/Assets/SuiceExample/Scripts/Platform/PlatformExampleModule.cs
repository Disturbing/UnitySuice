using DTools.Suice;

namespace SuiceExample.Platform
{
    public class PlatformExampleModule : AbstractModule
    {
        protected override void Configure()
        {
#if UNITY_IOS
            Bind<IPlatformExample>().To<iOSPlatformExample>().In(Scope.EAGER_SINGLETON);

#elif UNITY_ANDROID
            Bind<IPlatformExample>().To<AndroidPlatformExample>().In(Scope.EAGER_SINGLETON);

#else
            Bind<IPlatformExample>().To<DefaultPlatformExample>().In(Scope.EAGER_SINGLETON);
#endif
        }
    }
}
