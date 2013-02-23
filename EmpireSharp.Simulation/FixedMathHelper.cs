using FixMath.NET;

namespace EmpireSharp.Simulation
{

	public static class FixedMathHelper
	{

		public static Fix16 Lerp(Fix16 value1, Fix16 value2, Fix16 amount)
		{
			return value1 + (value2 - value1) * amount;
		}

	}

}
