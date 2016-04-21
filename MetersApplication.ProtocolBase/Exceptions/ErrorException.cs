using System;

namespace MetersApplication.ProtocolBase.Exceptions
{
    public class ErrorException : Exception
    {
        public ErrorException(string message)
            :base(message)
        {
        }

        public ErrorException()
            :base()
        {
        }
    }
}
