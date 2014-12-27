using DTools.Suice;

namespace SuiceExample.Snowman
{
    /// <summary>
    /// Randomly spawns a snowman then moves it.  Once moved, it will then be destroyed and spawn another.
    /// 
    /// @author DisTurBinG
    /// </summary>
    [ImplementedBy(typeof(SnowmanRandomSpawner))]
    public interface ISnowmanRandomSpawner
    {
        void Start();
    }
}