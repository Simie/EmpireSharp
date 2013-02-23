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

	public static class FixedMathHelper
	{

		public static Fix16 Lerp(Fix16 value1, Fix16 value2, Fix16 amount)
		{
			return value1 + (value2 - value1) * amount;
		}

	}

}
