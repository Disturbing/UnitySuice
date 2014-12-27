using System;
using DTools.Suice;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnitySuiceCommons.Resource
{
    /// <summary>
    /// Wrapper Unity Resources and Unity's GameObject to make it testable
    /// 
    /// @author DisTurBinG
    /// </summary>
    [Singleton]
    public class UnityResources : IUnityResources
    {
        public GameObject CreateEmptyGameObject(string objectName)
        {
            return new GameObject(objectName);
        }

        public GameObject CreatePrimitive(PrimitiveType primitiveType)
        {
            return GameObject.CreatePrimitive(primitiveType);
        }

        public Object Instantiate(GameObject original)
        {
            return Object.Instantiate(original);
        }

        public Object Load(string path, Type objectType)
        {
            return Resources.Load(path, objectType);
        }

        public T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }

        public ResourceRequest LoadAsync<T>(string path) where T : Object
        {
            return Resources.LoadAsync<T>(path);
        }
    }
}

