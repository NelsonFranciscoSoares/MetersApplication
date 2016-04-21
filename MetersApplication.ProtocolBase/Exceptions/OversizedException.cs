using System;

namespace MetersApplication.ProtocolBase.Exceptions
{
    public class OversizedException : Exception
    {
        public OversizedException(string message)
            : base(message)
        {
        }

        public OversizedException()
            : base()
        {
        }
    }
}
