using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace MetersApplication.Utils
{
    //Class that does conversions
    public static class ConvertUtils
    {
        public static byte[] ConvertTextToByteArrayInHexadecimal(string str)
        {
            var hexadecimal = TextToHexadecimal(str);
            return StringInHexadecimalToByteArray(hexadecimal);
        }


        public static string ConvertByteArrayInHexadecimalToText(byte[] data, int size)
        {
            var dataString = ByteArrayToStringInHexadecimal(data.Take(size).ToArray());
            return HexadecimalToText(dataString);
        }

        public static int ConvertByteInHexadecimalToInt(byte data)
        {
            var dataString = ByteArrayToStringInHexadecimal(new byte[]{data});
            return HexadecimalToInt32(dataString);
        }

        public static double ConvertByteArrayInHexadecimalToDouble(byte[] data)
        {
            var dataString = ByteArrayToStringInHexadecimal(data);
            return HexadecimalToInt64(dataString);
        }

        public static byte[] ConvertDateTimeToByteArrayInHexadecimal(DateTime dateTime)
        {
            var yearInBinary = ConvertIntToBinary(dateTime.Year, 16, 4);
            var monthInBinary = ConvertIntToBinary(dateTime.Month, 8, 4);
            var dayInBinary = ConvertIntToBinary(dateTime.Day, 8, 3);
            var hourInBinary = ConvertIntToBinary(dateTime.Hour,8, 3);
            var minutesInBinary = ConvertIntToBinary(dateTime.Minute, 8, 2);
            var secondsInBinary = ConvertIntToBinary(dateTime.Second,8, 2);
            var padding = "11";

            var dateInBinary = String.Format("{0}{1}{2}{3}{4}{5}{6}", yearInBinary, monthInBinary, dayInBinary,
                hourInBinary, minutesInBinary, secondsInBinary, padding);

            var hexadecimal = BinaryStringToHexString(dateInBinary);

            return StringInHexadecimalToByteArray(hexadecimal);
        }

        public static DateTime ConvertByteArrayInHexadecimalToDateTime(byte[] data)
        {
            var stringHexadecimal = ByteArrayToStringInHexadecimal(data);
            var binaryString = HexadecimalStringToBinaryString(stringHexadecimal);

            var year = ConvertBinaryToInt(binaryString, 0, 12);
            var month = ConvertBinaryToInt(binaryString, 12, 4);
            var day = ConvertBinaryToInt(binaryString, 16, 5);
            var hour = ConvertBinaryToInt(binaryString, 21, 5);
            var minutes = ConvertBinaryToInt(binaryString, 26, 6);
            var seconds = ConvertBinaryToInt(binaryString, 32, 6);

            return new DateTime(year, month, day, hour, minutes, seconds);
        }

        private static int ConvertBinaryToInt(string binary, int index, int length)
        {
            var binarySplitted = binary.Substring(index, length);
            return Convert.ToInt32(binarySplitted, 2);
        }

        private static string ConvertIntToBinary(int value, int nrBits, int offset)
        {
            var valueInBinary = Convert.ToString(value, 2);
            valueInBinary = valueInBinary.PadLeft(nrBits, '0');
            return valueInBinary.Substring(offset, valueInBinary.Length - offset);
        }

        //Convert Byte[] to String Hexadecimal
        private static string ByteArrayToStringInHexadecimal(byte[] data)
        {
            StringBuilder hex = new StringBuilder(data.Length * 2);
            foreach (byte b in data)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        //Convert String Hexadecimal to Byte[]
        private static byte[] StringInHexadecimalToByteArray(string hexadecimal)
        {
            return Enumerable.Range(0, hexadecimal.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hexadecimal.Substring(x, 2), 16))
                             .ToArray();
        }

        private static string TextToHexadecimal(string str)
        {
            var hexadecimal = String.Empty;

            foreach(var ch in str)
            {
                int value = Convert.ToInt32(ch);
                hexadecimal += String.Format("{0:X}", value);
            }
            return hexadecimal;
        }

        //Convert String Hexadecimal to Text
        private static string HexadecimalToText(string hexadecimal)
        {
            int numberChars = hexadecimal.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexadecimal.Substring(i, 2), 16);
            }
            return Encoding.Unicode.GetString(bytes);
        }

        //Convert String Hexadecimal to Int32
        private static int HexadecimalToInt32(string hexadecimal)
        {
            return Int32.Parse(hexadecimal, NumberStyles.HexNumber);
        }

        //Convert String Hexadecimal to Int64
        private static double HexadecimalToInt64(string hexadecimal)
        {
            return Int64.Parse(hexadecimal, NumberStyles.HexNumber);
        }

        private static string BinaryStringToHexString(string binary)
        {
            StringBuilder result = new StringBuilder(binary.Length / 8 + 1);

            // TODO: check all 1's or 0's... Will throw otherwise

            int mod4Len = binary.Length % 8;
            if (mod4Len != 0)
            {
                // pad to length multiple of 8
                binary = binary.PadLeft(((binary.Length / 8) + 1) * 8, '0');
            }

            for (int i = 0; i < binary.Length; i += 8)
            {
                string eightBits = binary.Substring(i, 8);
                result.AppendFormat("{0:X2}", Convert.ToByte(eightBits, 2));
            }

            return result.ToString();
        }

        private static string HexadecimalStringToBinaryString(string hexadecimal)
        {
            return Convert.ToString(Convert.ToInt32(hexadecimal, 16), 2);
        }

    }
}
