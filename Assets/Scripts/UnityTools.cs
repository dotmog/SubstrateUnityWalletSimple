using System;
using System.Globalization;
using System.Numerics;
using UnityEngine;

namespace Assets.Scripts
{
    public class UnityTools
    {
        public static string DmogDecimalString(BigInteger value, string formatting)
        {
            return ((double)value / 1000000000000000).ToString(formatting, CultureInfo.InvariantCulture);
        }
        public static string GetRomanNumber(int number)
        {
            if (number < 0 || number > 3999) 
                throw new NotImplementedException("Value for roman conversion to big");

            if (number < 1) 
                return "";

            if (number >= 1000) 
                return "M" + GetRomanNumber(number - 1000);
            if (number >= 900) 
                return "CM" + GetRomanNumber(number - 900);
            if (number >= 500) 
                return "D" + GetRomanNumber(number - 500);
            if (number >= 400) 
                return "CD" + GetRomanNumber(number - 400);
            if (number >= 100) 
                return "C" + GetRomanNumber(number - 100);
            if (number >= 90) 
                return "XC" + GetRomanNumber(number - 90);
            if (number >= 50) 
                return "L" + GetRomanNumber(number - 50);
            if (number >= 40) 
                return "XL" + GetRomanNumber(number - 40);
            if (number >= 10) 
                return "X" + GetRomanNumber(number - 10);
            if (number >= 9) 
                return "IX" + GetRomanNumber(number - 9);
            if (number >= 5) 
                return "V" + GetRomanNumber(number - 5);
            if (number >= 4) 
                return "IV" + GetRomanNumber(number - 4);
            if (number >= 1) 
                return "I" + GetRomanNumber(number - 1);
            
            return "";
        }
    }
}
