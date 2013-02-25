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
using System.Threading.Tasks;
using EmpireSharp.Simulation.Entities;
using Ninject;

namespace EmpireSharp.Simulation
{

	/// <summary>
	/// Contains a list of all the entities in the simulation.
	/// </summary>
	public class EntityContainer
	{

		private List<Entities.BaseEntity> _internalList;
		private uint _nextEntityID;

		public IList<Entities.BaseEntity> Entities { get { return _internalList.AsReadOnly(); } }
			
		[Inject]
		public IKernel Kernel { get; private set; }

		public EntityContainer()
		{
			_internalList = new List<BaseEntity>(1024);
		}

		public T CreateEntity<T>(FixedVector2? initialPosition = null) where T : Entities.BaseEntity, new()
		{

			var entity = new T();
			entity.EntityID = ++_nextEntityID;
			Kernel.Inject(entity);

			if(initialPosition.HasValue)
				entity.Transform.Position = initialPosition.Value;

			_internalList.Add(entity);

			entity.Init();

			return entity;

		}

		internal void Tick()
		{

			// Reverse loop in case entities are added during the tick.
			for (int i = _internalList.Count-1; i >= 0; --i) {
				_internalList[i].Tick();
			}

		}

	}

}
