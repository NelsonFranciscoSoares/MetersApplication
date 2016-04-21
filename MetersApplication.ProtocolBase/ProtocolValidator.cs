using MetersApplication.ProtocolBase.Constants;
using MetersApplication.ProtocolBase.Exceptions;
using MetersApplication.Utils;

namespace MetersApplication.ProtocolBase
{
    public class ProtocolValidator
    {
        public void ValidateFrame(byte[] frame, int bytesReceived, byte operation)
        {
            if ((bytesReceived != 5 
                    && (operation == MetersOperationsConstants.RESPONSE_CONNECT 
                        || operation == MetersOperationsConstants.RESPONSE_DISCONNECT
                        || operation == MetersOperationsConstants.ERROR)))
            {
                throw new OversizedException("The number of bytes received were not expected");
            }

            if((operation == MetersOperationsConstants.RESPONSE_SERIAL_NUMBER || operation == MetersOperationsConstants.RESPONSE_DATE_AND_TIME) 
                    && ConvertUtils.ConvertByteInHexadecimalToInt(frame[2]) != (bytesReceived - 4))
            {
                throw new OversizedException("The number of bytes received were not expected");
            }

            if(operation == MetersOperationsConstants.RESPONSE_READ_ENERGY_VALUE 
                    && ConvertUtils.ConvertByteInHexadecimalToInt(frame[2]) != (bytesReceived - 5))
            {
                 throw new OversizedException("The number of bytes received were not expected");
            }

            if (frame[1] == MetersOperationsConstants.ERROR)
                throw new ErrorException("There was an error");
                
            if (frame[1] != operation)
                throw new InvalidFormatException("Function code is not correct");

            var checkSum = FrameUtils.CalcChecksum(frame, bytesReceived);

            if (checkSum != frame[bytesReceived - 1])
                throw new ChecksumErrorException("Checksum is not correct");
        }

    }
}
