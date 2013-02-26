/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/
using System;

namespace EmpireSharp.Windows.Framework.Services
{
	public interface ILog
	{

		void LogException(Exception e);
		void LogError(string err, params object[] args);
		void LogWarning(string wrn, params object[] args);
		void Log(string log, params object[] args);

	}
}