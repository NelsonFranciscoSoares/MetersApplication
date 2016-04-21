namespace MetersApplication.ProtocolBase.Constants
{
    public class MetersOperationsConstants
    {
        public const byte REQUEST_CONNECT = 0x01;
        public const byte RESPONSE_CONNECT = 0x65;
        public const byte REQUEST_DISCONNECT = 0x06;
        public const byte RESPONSE_DISCONNECT = 0x68;
        public const byte REQUEST_SERIAL_NUMBER = 0x02;
        public const byte RESPONSE_SERIAL_NUMBER = 0x66;
        public const byte REQUEST_DATE_AND_TIME = 0x04;
        public const byte RESPONSE_DATE_AND_TIME = 0x67;
        public const byte REQUEST_READ_ENERGY_VALUE = 0x05;
        public const byte RESPONSE_READ_ENERGY_VALUE = 0x69;
        public const byte ERROR = 0xFF;
    }
}
