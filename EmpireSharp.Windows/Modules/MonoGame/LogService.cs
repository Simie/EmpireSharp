/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using System;
using EmpireSharp.Windows.Framework.Services;

namespace EmpireSharp.Windows.Modules.MonoGame
{
	class LogService : ILog
	{

		public void LogException(Exception e)
		{
			Console.WriteLine(e.ToString());
		}

		public void LogError(string err, params object[] args)
		{
			Console.WriteLine("Error: {0}", string.Format(err, args));
		}

		public void LogWarning(string wrn, params object[] args)
		{
			Console.WriteLine("Warning: {0}", string.Format(wrn, args));
		}

		public void Log(string log, params object[] args)
		{
			Console.WriteLine("Error: {0}", string.Format(log, args));
		}

	}
}
