using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmpireSharp.Simulation.Entities;

namespace EmpireSharp.Simulation
{

	/// <summary>
	/// Contains a list of all the entities in the simulation.
	/// </summary>
	public class EntityContainer
	{

		private List<Entities.BaseEntity> _internalList;

		public EntityContainer()
		{
			_internalList = new List<BaseEntity>(1024);
		}

		internal void Tick()
		{

			for (int i = 0; i < _internalList.Count; i++) {
				_internalList[i].Tick();
			}

		}

	}

}
