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

		[Inject]
		protected Time Time { get; private set; }

		public virtual void Tick() {}

	}

}
