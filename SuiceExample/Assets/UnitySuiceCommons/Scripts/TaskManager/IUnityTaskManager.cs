using System;
using UnityEngine;

namespace UnitySuiceCommons.TaskManager
{
    /// <summary>
    /// @author DisTurBinG
    /// </summary>
    public interface IUnityTaskManager
    {
        void AddRepetableTask(Func<bool> taskFunction, double delayInMillis, bool runNow = false);
        void AddTask(Action taskFunction, double delayInMillis = 0, bool deleteDuplicateTask = false);
        void LoadWWW(string url, Action<WWW> onCompleteCallback);
    }
}