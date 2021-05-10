using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamInventoryNotifier.Extensions
{
    public static class NumericExtensions
    {
        public static int GetDigitsQuantity(this int value)
        {
            return value != 0 ?
                (int) Math.Floor(Math.Log10(value) + 1) :
                1;
        }

        public static int GetDigitsQuantity(this long value)
        {
            return value != 0 ?
                (int)Math.Floor(Math.Log10(value) + 1) :
                1;
        }
    }
}
