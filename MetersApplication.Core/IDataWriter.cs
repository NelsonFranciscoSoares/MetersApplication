using System;
using System.Collections.Generic;
using MetersApplication.DataModel;

namespace MetersApplication.Core
{
    public interface IDataWriter : IDisposable
    {
        void Write(IEnumerable<MetersInformation> data);
    }
}
