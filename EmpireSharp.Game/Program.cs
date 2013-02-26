/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;
using EmpireSharp.Windows.Modules.MonoGame;

#endregion

namespace EmpireSharp.Windows
{
	/// <summary>
	/// The main class.
	/// </summary>
	public static class Program
	{
		private static Main game;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			game = new Main();
			game.Run();
		}
	}
}
