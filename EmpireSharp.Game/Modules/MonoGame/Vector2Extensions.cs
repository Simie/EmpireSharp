/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using EmpireSharp.Simulation;
using Microsoft.Xna.Framework;

namespace EmpireSharp.Game.Modules.MonoGame
{
	public static class Vector2Extensions
	{

		const string FormatString = " 0.00;-0.00";

		public static string ShortString(this Vector2 vec)
		{
			return string.Format("{0}, {1}", vec.X.ToString(FormatString), vec.Y.ToString(FormatString));
		}

		public static string ShortString(this FixedVector2 vec)
		{

			return string.Format("{0}, {1}", ((float)vec.X).ToString(FormatString), ((float)vec.Y).ToString(FormatString));

		}

	}
}
