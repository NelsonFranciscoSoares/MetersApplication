using System;

namespace MetersApplication.ProtocolBase.Exceptions
{
    public class InvalidFormatException : Exception
    {
        public InvalidFormatException(string message)
            : base(message)
        {
        }

        public InvalidFormatException()
            : base()
        {
        }
    }
}
