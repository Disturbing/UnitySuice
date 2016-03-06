using DTools.Suice;
using SuiceExample.Factory;

namespace SuiceExample.Snowman
{
    /// <summary>
    /// Creates Snowman Objects!  They may be created via injecting ISnowmanController or injecting this factory and manually calling the provide method.
    /// 
    /// @author DisTurBinG
    /// </summary>
    [ImplementedBy(typeof(SnowmanPoolManager))]
    public interface ISnowmanPoolManager
    {
        void ReturnToPool(ISnowmanController component);
        new ISnowmanController Provide();
    }
}
