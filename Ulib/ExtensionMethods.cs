using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ULib
{
   public static class ExtensionMethods
    {
        public static int ToInt(this object In) {
            return Convert.ToInt32(In);
        }
        public static long ToLong(this object In)
        {
            return Convert.ToInt64(In);
        }
        public static short ToShort(this object In)
        {
            return Convert.ToInt16(In);
        }
        public static ulong ToUlong(this object In)
        {
            return Convert.ToUInt64(In);
        }
    }
}
