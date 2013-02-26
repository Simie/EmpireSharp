/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/


namespace EmpireSharp.Simulation
{

	public class DataService
	{

		/// <summary>
		/// Active papyrus database.
		/// </summary>
		public Papyrus.RecordDatabase Database { get; private set; }

		internal void Init(Papyrus.RecordDatabase database)
		{
			Database = database;
		}

	}

}
