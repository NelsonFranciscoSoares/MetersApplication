using System;
using System.Collections.Generic;

namespace MetersApplication.Core
{
    public interface IDataReader : IDisposable
    {
        Dictionary<int, string> ReadFile();
    }
}
