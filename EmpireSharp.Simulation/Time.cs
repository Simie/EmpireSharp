/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/
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
