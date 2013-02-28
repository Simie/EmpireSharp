/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/


using System.Diagnostics;
using EmpireSharp.Simulation.Entities;

namespace EmpireSharp.Simulation.Behaviours
{

	class MoveBehaviour : Behaviour
	{

		public FixedVector2 TargetPosition { get; private set; }

		public Unit Unit { get; private set; }

		public MoveBehaviour(FixedVector2 targetPosition)
		{
			TargetPosition = targetPosition;
		}

		public override void Init(BaseEntity entity)
		{
			Debug.Assert(entity is Unit, "MoveBehaviour can only apply to a unit.");
			Unit = (Unit) entity;
		}

		public override void Tick()
		{

			if (Unit.Transform.Position == TargetPosition) {
				Complete();
				return;
			}

			// How far this unit moves per tick.
			var moveStep = Unit.MoveSpeed*Unit.Time.TimeStep;

			var destDirection = TargetPosition - Unit.Transform.Position;
			var destDistance = destDirection.Length();
			destDirection.Normalize();

			if (destDistance < moveStep) {
				Unit.Transform.Position = TargetPosition;
				Complete();
				return;
			}

			Unit.Transform.Position += destDirection*moveStep;
			Unit.Transform.Rotation = destDirection.Angle();

		}

		public override void Cancel()
		{
			
		}

	}

}
