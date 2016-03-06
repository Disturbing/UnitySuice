using System;
using DTools.Suice;
using UnityEngine;
using UnitySuiceCommons.TaskManager;

namespace SuiceExample.Task
{
    /// <summary>
    /// Used to show example of how to use co-routines in Suice.
    /// 
    /// @author DisTurBinG
    /// </summary>
    [Singleton(Scope.EAGER_SINGLETON)]
    public class TaskExample : IInitializable, ITaskExample
    {
        private const string TASK_MANAGER_END_MSG = "Task Manager Example Ends in {0}";
        private const int REPEAT_END_MSG_CNT = 3;

        private readonly IUnityTaskManager taskManager;

        [Inject]
        public TaskExample(IUnityTaskManager taskManager)
        {
            this.taskManager = taskManager;
        }

        public void Initialize()
        {
            Debug.Log("TaskExample initialized!");
            taskManager.AddTask(DelayedMessage, TimeSpan.FromSeconds(5).TotalMilliseconds);
        }

        private void DelayedMessage()
        {
            Debug.Log("Executing standard delay message after 5 seconds!");
            ScheduleRepeatMessage();
        }

        private void ScheduleRepeatMessage()
        {
            int countLeft = REPEAT_END_MSG_CNT;
            
            taskManager.AddRepetableTask(() => {
                Debug.Log(string.Format(TASK_MANAGER_END_MSG, countLeft));

                // Will repeat only while returning true!
                return --countLeft > 0;
            }, TimeSpan.FromSeconds(1).TotalMilliseconds); 
        }
    }
}
