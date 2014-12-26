using DTools.Suice;

namespace SuiceExample.Task
{
    /// <summary>
    /// Used to show example of how to use co-routines in Suice.
    /// 
    /// @author DisTurBinG
    /// </summary>
    [ImplementedBy(typeof(TaskExample))]
    public interface ITaskExample
    {
        void Initialize();
    }
}