using System.Collections.Generic;

namespace MetersApplication.DataModel
{
    public class MetersInformation : MetersBaseInformation
    {
        public IEnumerable<MetersDetailInformation> Details { get; set; }
    }
}
