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

		/// <summary>
		/// Entity transform (position, rotation)
		/// </summary>
		public Transform Transform;

		#region Unit Attributes

		public Fix16 MoveSpeed { get; internal set; }

		#endregion

		/// <summary>
		/// Active behaviour applied to this unit.
		/// </summary>
		internal Behaviours.Behaviour ActiveBehaviour { get; private set; }

		/// <summary>
		/// The behaviour that this unit will default to if no other behaviour is set. (Stand Ground, Patrol etc)
		/// </summary>
		internal Behaviours.Behaviour IdleBehaviour { get; private set; }

		[Inject]
		protected Terrain Terrain { get; set; }

		public Unit()
		{
			// Debug data for units TODO: Load unit data from papyrus
			MoveSpeed = (Fix16)0.2f;
		}

		public override void Tick()
		{

			base.Tick();

			if (ActiveBehaviour != null) {

				ActiveBehaviour.Tick();

				if (ActiveBehaviour.IsCompleted)
					ActiveBehaviour = null; // TODO: Dispose of this somehow? Maybe recycle?

			} else if (IdleBehaviour != null) {
				
				IdleBehaviour.Tick();

			}

		}

		internal void SetBehaviour(Behaviours.Behaviour behaviour)
		{

			if(ActiveBehaviour != null)
				ActiveBehaviour.Cancel();

			ActiveBehaviour = behaviour;
			ActiveBehaviour.Init(this);

		}

		internal void SetIdleBehaviour(Behaviours.Behaviour behaviour)
		{

			IdleBehaviour = behaviour;

		}

	}

}
