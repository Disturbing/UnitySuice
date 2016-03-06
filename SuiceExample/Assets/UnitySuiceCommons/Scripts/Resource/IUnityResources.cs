using System;
using DTools.Suice;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnitySuiceCommons.Resource
{
    /// <summary>
    /// Wrapper Unity Resources to make it testable
    /// 
    /// @author DisTurBinG
    /// </summary>
    [ImplementedBy(typeof(UnityResources))]
    public interface IUnityResources
    {
        // TODO: This is not testable - Create GameObjectWrapper
        GameObject CreateEmptyGameObject(string objectName);
        //TODO: This is not testable - create GameObjectWrapper
        GameObject CreatePrimitive(PrimitiveType primitiveType);
        //TODO: This is not testable - create GameObejctWrappers
        Object Instantiate(Object original);

        Object Load(string path, Type objectType);
        T Load<T>(string path) where T : Object;
        ResourceRequest LoadAsync<T>(string path) where T : Object;
    }
}