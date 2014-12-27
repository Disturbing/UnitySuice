using System;
using SuiceExample.Snowman;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SuiceExample.Factory
{
    /// <summary>
    /// Controller for the snowman component.  If this was an MVC patter, this would literally be the controller for the 'view' SnowmanComponent.
    /// If this were done more properly - there should be some kind of game obejct / monobehaviour controller parent to manage all capabilities in unity.
    /// 
    /// @author DisTurBinG
    /// </summary>
    public class SnowmanController : ISnowmanController
    {
        public class CannotDestroyWhileMovingException : Exception { }

        public static readonly Vector3 MIN_TARGET_MOVE_RANGE = Vector3.zero;
        public static readonly Vector3 MAX_TARGET_MOVE_RANGE = Vector3.one * 10;

        private readonly ISnowmanFactory snowmanFactory;
        private readonly ISnowmanMoveComponent moveComponent;

        public SnowmanController(ISnowmanFactory snowmanFactory, ISnowmanMoveComponent moveComponent)
        {
            this.snowmanFactory = snowmanFactory;
            this.moveComponent = moveComponent;
        }

        public void MoveToRandomPosition(float time)
        {
            moveComponent.WalkToPosition(new Vector3(
                Random.Range(MIN_TARGET_MOVE_RANGE.x, MAX_TARGET_MOVE_RANGE.x),
                Random.Range(MIN_TARGET_MOVE_RANGE.y, MAX_TARGET_MOVE_RANGE.y),
                Random.Range(MIN_TARGET_MOVE_RANGE.z, MAX_TARGET_MOVE_RANGE.z)), time);
        }

        public void Destroy()
        {
            if (IsMoving()) {
                throw new CannotDestroyWhileMovingException();
            }

            snowmanFactory.ReturnToPool(this);                
        }

        public bool IsMoving()
        {
            return moveComponent.IsMoving();
        }

        /// <summary>
        /// This would be good to move into a common game object controllers
        /// </summary>
        /// <param name="transform"></param>
        public void SetPosition(Vector3 position)
        {
            moveComponent.gameObject.transform.position = position;
        }

        /// <summary>
        /// This would be good to move into a common game object controllers
        /// </summary>
        /// <param name="transform"></param>
        public void SetActive(bool active)
        {
            moveComponent.gameObject.SetActive(active);
        }

        /// <summary>
        /// This would be good to move into a common game object controllers
        /// </summary>
        /// <param name="transform"></param>
        public void SetParentTransform(Transform transform)
        {
            moveComponent.gameObject.transform.parent = transform;
        }
    }
}
