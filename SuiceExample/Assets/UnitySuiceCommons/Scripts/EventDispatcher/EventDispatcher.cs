using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnitySuiceCommons.EventDispatcher.Exception;

namespace UnitySuiceCommons.EventDispatcher
{
    /// <summary>
    /// Dispatches custom events to methods flagged with EventListener attribute.
    /// Must register list of event types in constructor, which implement interface IDispatchedEvent.
    /// 
    /// @author DisTurBinG
    /// </summary>
    public class EventDispatcher
    {
        private readonly Dictionary<Type, List<EventListenerContainer>> eventListenerContainerMap = new Dictionary<Type, List<EventListenerContainer>>();

        private readonly HashSet<object> registeredEventListeners = new HashSet<object>(); 

        private readonly object[] objectContainer = new object[1];

        private struct EventListenerContainer
        {
            internal readonly object Instance;
            internal readonly MethodInfo EventListenerMethod;

            internal EventListenerContainer(object instance, MethodInfo eventListenerMethod)
            {
                Instance = instance;
                EventListenerMethod = eventListenerMethod;
            }
        }

        public bool AddEventListener(object eventListenerInstance)
        {
            bool successfullyAdded = false;

            if (!registeredEventListeners.Contains(eventListenerInstance))
            {
                successfullyAdded = AttemptToRegisterEventListener(eventListenerInstance);
            }

            return successfullyAdded;
        }

        private bool AttemptToRegisterEventListener(object eventListenerInstance)
        {
            const BindingFlags eventMethodFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            bool registeredEventListener = false;

            foreach (MethodInfo methodInfo in eventListenerInstance.GetType().GetMethods(eventMethodFlags)) {
                if (Attribute.GetCustomAttribute(methodInfo, typeof (EventListener)) != null) {
                    if (methodInfo.IsPublic) {
                        registeredEventListener = RegisterMethodToEvent(eventListenerInstance, methodInfo);
                    } else {
                        throw new EventListenerMethodMustBePublic(eventListenerInstance.GetType().FullName, methodInfo.Name);
                    }
                }
            }

            if (registeredEventListener) {
                registeredEventListeners.Add(eventListenerInstance);
            }

            return registeredEventListener;
        }

        private bool RegisterMethodToEvent(object eventListenerInstance, MethodInfo methodInfo)
        {
            const bool registeredMethod = true;
            
            if (methodInfo.GetParameters().Length == 1) {
                Type eventType = methodInfo.GetParameters()[0].ParameterType;
                List<EventListenerContainer> eventListeners = eventListenerContainerMap.ContainsKey(eventType)
                    ? eventListenerContainerMap[eventType]
                    : eventListenerContainerMap[eventType] = new List<EventListenerContainer>();

                eventListeners.Add(new EventListenerContainer(eventListenerInstance, methodInfo));
            } else {
                throw new InvalidEventListenerParamException(eventListenerInstance.GetType().FullName,
                                             methodInfo.Name);
            }

            return registeredMethod;
        }

        public bool RemoveEventListener(object eventListenerInstance)
        {
            bool removedEventListener = registeredEventListeners.Remove(eventListenerInstance);

            if (removedEventListener) {
                foreach (List<EventListenerContainer> eventListenerInstances in eventListenerContainerMap.Values) {
                    eventListenerInstances.RemoveAll(e => e.Instance == eventListenerInstance);
                }
            }
          
            return removedEventListener;
        }

        public void BroadcastEvent(IDispatchedEvent dispatchedEventToBroadcast)
        {
            List<EventListenerContainer> eventListeners;

            if (eventListenerContainerMap.TryGetValue(dispatchedEventToBroadcast.GetType(), out eventListeners)) {
                foreach (EventListenerContainer eventListener in eventListeners) {
                    objectContainer[0] = dispatchedEventToBroadcast;
                    eventListener.EventListenerMethod.Invoke(eventListener.Instance, objectContainer);
                }
            }
        }
    }
}
