using System;
using DTools.Suice;
using UnitySuiceCommons.TaskManager;

namespace SuiceExample.Snowman
{
    /// <summary>
    /// Randomly spawns a snowman then moves it.  Once moved, it will then be destroyed and spawn another.
    /// 
    /// @author DisTurBinG
    /// </summary>
    [Singleton]
    public class SnowmanRandomSpawner : ISnowmanRandomSpawner
    {
        public const float MOVEMENT_TIME_LENGTH_S = 3;
        public const float DESTROY_DELAY_TIME_LENGTH_S = MOVEMENT_TIME_LENGTH_S + 5;

        public class AlreadyStartedException : Exception { }

        private readonly ISnowmanPoolManager snowmanPoolManager;
        private readonly IUnityTaskManager taskManager;

        private bool hasStarted = false;

        [Inject]
        public SnowmanRandomSpawner(ISnowmanPoolManager snowmanPoolManager, IUnityTaskManager taskManager)
        {
            this.snowmanPoolManager = snowmanPoolManager;
            this.taskManager = taskManager;
        }

        public void Start()
        {
            if (hasStarted) {
                throw new AlreadyStartedException();
            }

            hasStarted = true;
            taskManager.AddRepetableTask(SpawnSnowmanLoop, TimeSpan.FromSeconds(MOVEMENT_TIME_LENGTH_S).TotalMilliseconds);
        }

        private bool SpawnSnowmanLoop()
        {
            ISnowmanController snowmanController = snowmanPoolManager.Provide();
            snowmanController.MoveToRandomPosition(MOVEMENT_TIME_LENGTH_S);
            taskManager.AddTask(snowmanController.Destroy, TimeSpan.FromSeconds(DESTROY_DELAY_TIME_LENGTH_S).TotalMilliseconds);

            // Infinite Loop
            return true;
        }
    }
}
