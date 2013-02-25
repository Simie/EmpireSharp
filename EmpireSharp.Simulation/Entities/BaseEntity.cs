/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/
using Ninject;

namespace EmpireSharp.Simulation.Entities
{

	/// <summary>
	/// Abstract base class for an Entity
	/// </summary>
	public abstract class BaseEntity
	{

		/// <summary>
		/// Entity transform (position, rotation)
		/// </summary>
		public Transform Transform;

		/// <summary>
		/// Unique ID of this entity, assigned on creation.
		/// </summary>
		public uint EntityID { get; internal set; }

		[Inject]
		protected Time Time { get; private set; }

		/// <summary>
		/// Called after this entity has been assigned an ID, dependencies injected and initial position set.
		/// </summary>
		public virtual void Init() {}

		public virtual void Tick() {}

	}

}
