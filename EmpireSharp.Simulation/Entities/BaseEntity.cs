/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/
namespace EmpireSharp.Simulation.Entities
{

	/// <summary>
	/// Abstract base class for an Entity
	/// </summary>
	public abstract class BaseEntity
	{

		/// <summary>
		/// Will this entity block other entities movement
		/// </summary>
		public bool IsCollider { get; set; }

		/// <summary>
		/// Size of the collision circle around this entity
		/// </summary>
		public float CollisionRadius { get; set; }

	}

}
