using FixMath.NET;

namespace EmpireSharp.Simulation
{

	/// <summary>
	/// Time service
	/// </summary>
	public class Time
	{

		public Fix16 TimeStep { get; private set; }

		public int TotalTicks { get; private set; }

		public Time()
		{
			// approx 0.083333
			TimeStep = Fix16.FromRaw(5461);
		}

		internal void Tick()
		{
			++TotalTicks;
		}

	}

}
