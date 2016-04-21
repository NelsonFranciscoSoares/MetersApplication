using MetersApplication.ProtocolBase.Exceptions;

namespace MetersApplication.ProtocolBase
{
    //Class that has operations about Frame
    public static class FrameUtils
    {
        //Operation to calculate the frame checksum
        public static byte CalcChecksum(byte[] frame, int bytesReceived)
        {
            if (bytesReceived < 2)
                throw new OversizedException("Frame with incorrect dimension");

            var value = frame[1];

            for (var i = 1; i < bytesReceived-2; i++)
            {
                value = (byte)(value ^ frame[i + 1]);
            }

            return value;
        }
    }
}
