using System;

namespace MetersApplication.ProtocolBase.Exceptions
{
    public class ChecksumErrorException : Exception
    {
        public ChecksumErrorException(string message)
            : base(message)
        {
        }

        public ChecksumErrorException()
            : base()
        {
        }
    }
}
