using UnityEngine;

namespace SuiceExample.Factory
{
    /// <summary>
    /// Interface for Snowman View (3D object in scene)
    /// It would be best to have this extend some common monobehaviour/gameobject interface
    /// 
    /// @author DisTurBinG
    /// </summary>
    public interface ISnowmanMoveComponent
    {
        GameObject gameObject { get; }
        void WalkToPosition(Vector3 targetMovePosition, float timeToGetToPosition);
        bool IsMoving();
    }
}