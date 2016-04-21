using System.Globalization;
using MetersApplication.ProtocolBase.Constants;

namespace MetersApplication.ProtocolBase.Factory
{
    public static class FrameFactory
    {
        //Method used to tests
        public static void Create(byte[] frame, byte functionCode, byte dimension, byte data)
        {
            frame[0] = MetersFrameConstants.FRAME_HEADER; //Header
            frame[1] = functionCode; //Function
            frame[2] = dimension; //Dimension
            frame[3] = data; //Data

            frame[4] = FrameUtils.CalcChecksum(frame, 5); //Checksum     
        }

        public static void Create(byte[] frame, byte functionCode, byte[] data)
        {
            frame[0] = MetersFrameConstants.FRAME_HEADER; //Header
            frame[1] = functionCode; //Function
            frame[2] = byte.Parse(data.Length.ToString(), NumberStyles.HexNumber); //Dimension

            for (var i = 0; i < data.Length; i++)
            {
                frame[i + 3] = data[i];
            }

            frame[data.Length + 3] = FrameUtils.CalcChecksum(frame, (data.Length + 4)); //Checksum
        }        

        public static byte[] Create(byte functionCode)
        {
            var frame = new byte[5];

            frame[0] = MetersFrameConstants.FRAME_HEADER; //Header
            frame[1] = functionCode; //Function
            frame[2] = 0x00; //Dimension
            frame[3] = 0x00; //Data
            frame[4] = FrameUtils.CalcChecksum(frame, 5); //Checksum

            return frame;
        }

        public static byte[] Create(byte functionCode, byte[] data)
        {
            var frame = new byte[5 + data.Length];

            frame[0] = MetersFrameConstants.FRAME_HEADER; //Header
            frame[1] = functionCode; //Function
            frame[2] = byte.Parse((data.Length + 1).ToString(), NumberStyles.HexNumber); //Dimension
            frame[3] = 0x01; //Size

            for (var i = 0; i < data.Length; i++)
            {
                frame[i + 4] = data[i];
            }

            frame[frame.Length - 1] = FrameUtils.CalcChecksum(frame, (data.Length + 5)); //Checksum

            return frame;
        }        
    }
}
