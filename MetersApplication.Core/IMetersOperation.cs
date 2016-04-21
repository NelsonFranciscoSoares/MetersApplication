using System;

namespace MetersApplication.Core
{
    public interface IMetersOperation : IDisposable
    {
        void Connect();
        void Disconnect();
        string ReadSerialNumber();
        DateTime ReadDateTime();
        double ReadEnergyValue(DateTime dateTime);
    }
}
