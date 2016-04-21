using System.Collections.Generic;
using MetersApplication.DataModel;

namespace MetersApplication.Core
{
    public interface IParser
    {
        IEnumerable<MetersRequest> Parse(Dictionary<int, string> inputData);
    }
}
