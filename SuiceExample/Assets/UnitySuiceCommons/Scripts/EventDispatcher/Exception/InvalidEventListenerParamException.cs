namespace UnitySuiceCommons.EventDispatcher.Exception
{
    /// <summary>
    /// Thrown when event listener does have specify the a single parameter which inherits IDispatchedEvent
    /// 
    /// @author DisTurBinG
    /// </summary>
    public class InvalidEventListenerParamException : System.Exception
    {
        private const string exceptionMessage =
            "EventListener method {0}#{1} must have one parameter where the param inherits IDispatchedEvent";

        public InvalidEventListenerParamException(string typeName, string methodName)
            : base(string.Format(exceptionMessage, typeName, methodName)) { }
    }
}
