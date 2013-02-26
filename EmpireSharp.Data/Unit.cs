/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Papyrus;

namespace EmpireSharp.Data
{

	[Record]
	public class Unit : EmpireRecord
	{

		private string _name;

		[RecordProperty(1)]
		public string Name
		{
			get { return _name; }
			set
			{
				ThrowIfReadOnly("Name");
				_name = value;
				RaisePropertyChanged("Name");
			}
		}

	}

}
