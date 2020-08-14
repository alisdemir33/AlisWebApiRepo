using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VakifIlan
{
    public class Util
    {
        public static bool TcKontrol(string tcNo)
        {
            string c = tcNo;

            if (tcNo.Length != 11)
            {
                return false;
            }
            if (!isNumCheck(c))
            {
                return false;
            }

            int n = int.Parse(c.Substring(0, 1));
            if (n == 0)
            {
                return false;
            }
            int m = int.Parse(c.Substring(1, 1));
            int l = int.Parse(c.Substring(2, 1));
            int j = int.Parse(c.Substring(3, 1));
            int i = int.Parse(c.Substring(4, 1));
            int h = int.Parse(c.Substring(5, 1));
            int f = int.Parse(c.Substring(6, 1));
            int d = int.Parse(c.Substring(7, 1));
            int b = int.Parse(c.Substring(8, 1));
            int g = int.Parse(c.Substring(9, 1));
            int e = int.Parse(c.Substring(10, 1));
            if ((10 - ((n + l + i + f + b) * 3 + m + j + h + d) % 10) % 10 != g || (10 - ((m + j + h + d + g) * 3 + n + l + i + f + b) % 10) % 10 != e)
            {
                return false;
            }
            return true;

        }
        private static bool isNumCheck(string value)
        {

            if (!string.IsNullOrEmpty(value))
            {
                foreach (char chr in value)
                {
                    if (!Char.IsNumber(chr)) return false;

                }
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}