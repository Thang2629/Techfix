using System;

namespace TechFix.Common
{
	public static class DecimalUtil
	{
		public static decimal Round(this decimal d, int digits = 2, MidpointRounding rounding = MidpointRounding.ToNegativeInfinity)
		{
			return Math.Round(d, digits, rounding);
		}

		public static decimal RoundUp(this decimal d, int digits = 2)
		{
			return Math.Round(d, digits, MidpointRounding.ToPositiveInfinity);
		}

        public static decimal RoundOff(decimal i)
        {
            return Math.Round(i / 10.0m) * 10;
        }
	}
}
