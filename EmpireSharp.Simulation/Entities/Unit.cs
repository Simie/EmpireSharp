/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using FixMath.NET;
using Ninject;

namespace EmpireSharp.Simulation.Entities
{

	public class Unit : BaseEntity
	{

		/// <summary>
		/// Will this entity block other entities movement
		/// </summary>
		public bool IsCollider { get; set; }

		/// <summary>
		/// Size of the collision circle around this entity
		/// </summary>
		public Fix16 CollisionRadius { get; set; }

		[Inject]
		protected Terrain Terrain { get; set; }

		public override void Tick()
		{

			base.Tick();

			Transform.Position += FixedVector2.One * Time.TimeStep;

		}

	}

}
