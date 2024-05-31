namespace Components.Exceptions
{

    public enum DgcExceptionType
    {
        ResourceNotFound,
        InvalidArgument,
        ArgumentNull,
        ArgumentOutOfRange,
        InvalidOperation,
        NotSupported,
        Unauthorized,
        Forbidden
    }

    [Serializable]
    public class DgcException : Exception
    {
        public DgcExceptionType Type { get; set;}
        
        public DgcException(string message) : base(message) {}

        public DgcException(string message, DgcExceptionType type) : base(message)
        {
            Type = type;
        }
        
    }
}