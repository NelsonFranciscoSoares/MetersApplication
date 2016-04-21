using MetersApplication.DataModel;

namespace MetersApplication.Core
{
    public interface IQueue
    {
        void Put(MetersInformationFlatFormat data);
        void Flush();
    }
}
