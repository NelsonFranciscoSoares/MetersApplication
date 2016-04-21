using System;

namespace MetersApplication.DataModel
{
    [Serializable]
    public class MetersInformationFlatFormat : MetersBaseInformation
    {
        public DateTime RegisterDateTime { get; set; }
        public double EnergyValue { get; set; }
    }
}
