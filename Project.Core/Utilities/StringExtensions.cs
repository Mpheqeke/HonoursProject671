using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Project.Core.Utilities
{
    public static partial class Extensions
    {
        public static bool IsNullOrEmpty(
            this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNotNullOrEmpty(
            this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        public static bool IsNumeric(
            this string value)
        {
            if(int.TryParse(value, out int result)){
                return true;
            }
            return false;
            
        }
    }
}