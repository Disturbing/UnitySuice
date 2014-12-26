using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitySuiceCommons.TaskManager
{

    /// <summary>
    /// Generic task manager to execute repetable or single delayed tasks for services.
    /// This is some old code that can be massively optimized. Please suggest people :).
    /// 
    /// @author DisTurBinG
    /// </summary>
    public class UnityTaskManager : MonoBehaviour, IUnityTaskManager
    {
        private readonly List<Task> activeTasks = new List<Task>();

        /// <summary>
        /// Repetable tasks will repeat for every delay in milliseconds.
        /// The function passed must return a boolean.
        /// 
        /// --  If that boolean returns true, then the task will continued to repeat.
        /// --  Once the function returns false, the task will be removed from the task manager
        /// --  Option to run instantly, then repeat
        /// </summary>
        public void AddRepetableTask(Func<bool> taskFunction, double delayInMillis, bool runInstantly = false)
        {
            RepetableTask repetableTask = new RepetableTask(taskFunction, delayInMillis);

            if (runInstantly == false || repetableTask.InvokeTask()) {
                AddTask(repetableTask);
            }
        }

        /// <summary>
        /// Executes single task in specified delayed milliseconds.
        /// </summary>
        public void AddTask(Action taskFunction, double delayInMillis = 0L, bool deleteDuplicateTask = false)
        {
            SingleTask task = new SingleTask(taskFunction, delayInMillis);
            if (deleteDuplicateTask) {
                int count = activeTasks.RemoveAll(x => (x as SingleTask) != null && (x as SingleTask).Equals(task));
            }

            AddTask(task);
        }

        public void LoadWWW(string url, Action<WWW> onCompleteCallback)
        {
            StartCoroutine(AsyncLoadWWWOperation(url, onCompleteCallback));
        }

        private IEnumerator AsyncLoadWWWOperation(string url, Action<WWW> onCompleteCallback)
        {
            WWW www = new WWW(url);

            yield return www;

            onCompleteCallback(www);
        }

        private void AddTask(Task task)
        {
            task.NextStartTime = DateTime.Now.Ticks + task.Delay * TimeSpan.TicksPerMillisecond;
            activeTasks.Add(task);
            activeTasks.Sort();
        }

        /// <summary>
        /// Executes one task at a time when they are ready to be executed
        /// </summary>
        void Update()
        {
            if (activeTasks.Count > 0) {
                Task task = activeTasks[0];
                if (DateTime.Now.Ticks > task.NextStartTime) {
                    activeTasks.RemoveAt(0);

                    if (task.InvokeTask()) {
                        AddTask(task);
                    }
                }
            }
        }

        private abstract class Task : IComparable<Task>
        {
            internal double NextStartTime;
            internal readonly double Delay;

            internal Task(double delay)
            {
                Delay = delay;
            }

            public abstract bool InvokeTask();

            public int CompareTo(Task otherTask)
            {
                int result = 0;

                if (NextStartTime > otherTask.NextStartTime) {
                    result = 1;
                } else if (NextStartTime < otherTask.NextStartTime) {
                    result = -1;
                }

                return result;
            }
        }

        private class SingleTask : Task
        {
            internal readonly Action Task;

            public SingleTask(Action task, double delay)
                : base(delay)
            {
                Task = task;
            }

            public override bool InvokeTask()
            {
                Task.Invoke();
                return false;
            }

            public bool Equals(SingleTask task)
            {
                return Task.Equals(task.Task);
            }
        }

        private class RepetableTask : Task
        {
            internal readonly Func<bool> Task;
            internal long InvokeCount;

            public RepetableTask(Func<bool> task, double delay)
                : base(delay)
            {
                Task = task;
            }

            public override bool InvokeTask()
            {
                InvokeCount++;
                return Task.Invoke();
            }
        }
    }
}