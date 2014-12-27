using DTools.Suice;
using SuiceExample.Factory;
using UnityEngine;

namespace SuiceExample.Snowman
{
    /// <summary>
    /// Controller for the snowman component.  If this was an MVC patter, this would literally be the controller for the 'view' SnowmanComponent.
    /// If this were done more properly - there should be some kind of game obejct / monobehaviour controller parent to manage all capabilities in unity.
    /// 
    /// This interface is marked as a provided by attribute to a factory pattern.  This allows any dependency to inject this interface which will automatically
    /// take create a move controller.
    /// 
    /// @author DisTurBinG
    /// </summary>
    [ProvidedBy(typeof(ISnowmanFactory))]
    public interface ISnowmanController
    {
        void MoveToRandomPosition(float time);
        void SetPosition(Vector3 position);
        void Destroy();
        bool IsMoving();
        void SetActive(bool active);
        void SetParentTransform(Transform transform);
    }
}
