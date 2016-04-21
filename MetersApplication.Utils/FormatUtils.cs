using System;
using System.Text;

namespace MetersApplication.Utils
{
    public static class FormatUtils
    {
        public static double Round(double value)
        {
            var valueRoundedThreePointDecimal = Math.Round(value, 3).ToString();

            if (Int32.Parse(valueRoundedThreePointDecimal[valueRoundedThreePointDecimal.Length - 1].ToString()) != 5)
            {
                return Math.Round(value, 2);
            }
            else
            {
                var segundaCasaDecimal = Int32.Parse(valueRoundedThreePointDecimal[valueRoundedThreePointDecimal.Length - 2].ToString());

                if(segundaCasaDecimal % 2 != 0)
                {
                    ++segundaCasaDecimal;

                    var sb = new StringBuilder(Math.Round(value, 2).ToString());
                    sb.Remove(sb.Length - 1, 1);
                    sb.Insert(sb.Length, segundaCasaDecimal);

                    return double.Parse(sb.ToString());
                }
                else
                {
                    return Math.Round(value, 2);
                }
            }
        }
    }
}
