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

	public class EntityEventArgs : EventArgs
	{

		public enum Op
		{
			Added,
			Removed
		}

		public uint EntityID { get; internal set; }
		public Op Operation { get; internal set; }

	}

	/// <summary>
	/// Contains a list of all the entities in the simulation.
	/// </summary>
	public class EntityContainer
	{

		/// <summary>
		/// A fast list for iterating in order during an update.
		/// </summary>
		private List<Entities.BaseEntity> _internalList;

		/// <summary>
		/// Entity ID lookup.
		/// </summary>
		private Dictionary<uint, Entities.BaseEntity> _entityLookup; 

		private uint _nextEntityID;

		public IList<Entities.BaseEntity> Entities { get { return _internalList.AsReadOnly(); } }

		public event EventHandler<EntityEventArgs> EntityAdded; 
		public event EventHandler<EntityEventArgs> EntityRemoved; 

		/// <summary>
		/// Lookup an entity by ID
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Entities.BaseEntity this[uint id]
		{
			get { return _entityLookup[id]; }
		}

		[Inject]
		public IKernel Kernel { get; private set; }

		internal EntityContainer()
		{
			_internalList = new List<BaseEntity>(1024);
			_entityLookup = new Dictionary<uint, BaseEntity>(1024);
		}

		internal T CreateEntity<T>() where T : Entities.BaseEntity, new()
		{

			var entity = new T();
			entity.EntityID = ++_nextEntityID;
			Kernel.Inject(entity);

			_internalList.Add(entity);
			_entityLookup[_nextEntityID] = entity;

			entity.Init();

			OnEntityAdded(entity.EntityID);

			return entity;

		}

		internal void Tick()
		{

			// Reverse loop in case entities are added during the tick.
			for (int i = _internalList.Count-1; i >= 0; --i) {
				_internalList[i].Tick();
			}

		}

		protected void OnEntityAdded(uint entID)
		{
			
			if(EntityAdded != null)
				EntityAdded(this, new EntityEventArgs() { EntityID = entID, Operation = EntityEventArgs.Op.Added });

		}

	}

}
