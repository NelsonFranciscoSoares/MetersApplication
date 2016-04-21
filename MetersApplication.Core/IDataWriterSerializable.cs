using System.Collections.Generic;
using MetersApplication.DataModel;

namespace MetersApplication.Core
{
    public interface IDataWriterSerializable
    {
        void AppendSerializeObject(List<MetersInformationFlatFormat> data);
        List<MetersInformationFlatFormat> GetData();
    }
}
