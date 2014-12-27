using DTools.Suice;

namespace UnitySuiceCommons.UnityEvent
{
    /// <summary>
    /// We use interfaces all the time to not only allow testability, but to hide event calls so that other's may not accidently call it.
    /// 
    /// @author DisTurBinG
    /// </summary>
    [ImplementedBy(typeof(UnityEventExample))]
    public interface IUnityEventExample
    {

    }
}
