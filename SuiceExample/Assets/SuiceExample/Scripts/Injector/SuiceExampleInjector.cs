using SuiceExample.Platform;
using UnitySuiceCommons.Injector;

/// <summary>
/// @author DisTurBinG
/// </summary>
public class SuiceExampleInjector : UnityInjector {

    protected override void RegisterModules()
    {
        // Registers platform example module here - which is used to manually bind dependencies based on platform type
        Injector.RegisterModule(new PlatformExampleModule());
    }
}
