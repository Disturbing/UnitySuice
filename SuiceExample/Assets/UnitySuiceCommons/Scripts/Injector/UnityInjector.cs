using System.Reflection;
using CmnEventDispatcher.Util;
using UnitySuiceCommons.EventDispatcher;
using UnitySuiceCommons.TaskManager;
using UnityEngine;

namespace UnitySuiceCommons.Injector
{
    /// <summary>
    /// Abstract injector for unity projects which includes two common utilities:
    /// 
    /// 1) Global Event Dispatcher
    /// 2) Unity Task Manager
    /// 
    /// @author DisTurBinG
    /// </summary>
    public abstract class UnityInjector : MonoBehaviour
    {
        protected readonly DTools.Suice.Injector Injector = new DTools.Suice.Injector();

        private readonly GlobalEventDispatcherModule globalEventDispatcherModule = new GlobalEventDispatcherModule();

        /// <summary>
        /// Only necessary to make sure this class is not destroyed across scene changes
        /// </summary>
        void Awake()
        {
            DontDestroyOnLoad(gameObject);            
        }

        /// <summary>
        /// It's best to run setup injector on the start function so that we can show our own loading screen versus the splash screen.
        /// 
        /// Best practice is to pass the splash screen and show a loading bar as fast as possible.
        /// </summary>
        void Start()
        {
            SetupInjector();
        }

        protected abstract void RegisterModules();

        private void SetupInjector()
        {
            // Register All Modules
            RegisterUnitySuiceCommonsModules();
            RegisterModules();

            // Setup Event Listener suice registrations
            SetupEventListener();

            // Start suice with Unity dll - Options to start with mutliple assemblies are available
            Injector.Initialize(Assembly.GetExecutingAssembly());
        }

        private void SetupEventListener()
        {
            // Register All Suice Instances to listen to UnityEvents
            Injector.OnInitializeDependency += gameObject.AddComponent<UnityEventDispatcherComponent>().RegisterEventListener;
            // Register all Suice Instances to listen to Global Events
            Injector.OnInitializeDependency += globalEventDispatcherModule.GlobalEventDispatcher.RegisterListener;
        }

        private void RegisterUnitySuiceCommonsModules()
        {
            Injector.RegisterModule(new UnityTaskManagerModule(gameObject));
            Injector.RegisterModule(globalEventDispatcherModule);
        }
    }
}