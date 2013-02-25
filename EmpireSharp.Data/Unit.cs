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
