using System;
using DTools.Suice;
using UnitySuiceCommons.EventDispatcher;
using UnitySuiceCommons.EventDispatcher.Unity.UnityEvent;
using UnitySuiceCommons.TaskManager;

namespace SuiceExample.Snowman
{
    /// <summary>
    /// Initial snowman spawner class.  Once the two snowmen are spawned, it will begin the random spawner.
    /// 
    /// @author DisTurBinG
    /// </summary>
    [Singleton(Scope.EAGER_SINGLETON)]
    public class InitialSnowmanSpawner : IDoubleSnowmanSpawner
    {
        public const float SNOWMAN_MOVE_TIME_LENGTH = 5;

        private readonly ISnowmanController snowman1;
        private readonly ISnowmanController snowman2;
        private readonly IUnityTaskManager taskManager;
        private readonly ISnowmanRandomSpawner snowmanRandomSpawner;

        [Inject]
        public InitialSnowmanSpawner(ISnowmanController snowman1, ISnowmanController snowman2,
            IUnityTaskManager taskManager, ISnowmanRandomSpawner snowmanRandomSpawner)
        {
            this.snowman1 = snowman1;
            this.snowman2 = snowman2;
            this.taskManager = taskManager;
            this.snowmanRandomSpawner = snowmanRandomSpawner;
        }

        /// <summary>
        /// NOTICE - this is called on the OnStart event because we can only utilize other dependencies from the start layer.
        /// If we ran this in the initialization phase, we don't know if taskmanager has been initialized yet.
        /// Just like unity's Awake vs Start - you should treate initialize as awake and unordered.
        /// </summary>
        /// <param name="startEvent"></param>
        [EventListener]
        public void OnStart(UnityStartEvent startEvent)
        {
            snowman1.MoveToRandomPosition(SNOWMAN_MOVE_TIME_LENGTH);
            snowman2.MoveToRandomPosition(SNOWMAN_MOVE_TIME_LENGTH);

            taskManager.AddTask(snowmanRandomSpawner.Start, TimeSpan.FromSeconds(SNOWMAN_MOVE_TIME_LENGTH).TotalMilliseconds);
        }
    }
}
