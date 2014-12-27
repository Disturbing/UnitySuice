using System;
using SuiceExample.Snowman;
using UnityEngine;

namespace SuiceExample.Factory
{
    /// <summary>
    /// Moves snowman to particular location
    /// 
    /// @author DisTurBinG
    /// </summary>
    public class SnowmanMoveComponent : MonoBehaviour, ISnowmanMoveComponent
    {
        private Vector3 targetMovePosition;
        private Vector3 currentVelocity;
        private float timeToGetToPosition;

        public void WalkToPosition(Vector3 targetMovePosition, float timeToGetToPosition)
        {
            this.targetMovePosition = targetMovePosition;
            this.timeToGetToPosition = timeToGetToPosition;
        }


        public bool IsMoving()
        {
            return transform.position != targetMovePosition;
        }

        void Update()
        {
            if (IsMoving()) {
                transform.position = Vector3.MoveTowards(transform.position, targetMovePosition, timeToGetToPosition * Time.deltaTime);   
            }
        }
    }
}
