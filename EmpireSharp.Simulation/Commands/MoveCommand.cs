/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using System;
using EmpireSharp.Simulation.Entities;

namespace EmpireSharp.Simulation.Commands
{

	public class MoveCommand : Command
	{

		/// <summary>
		/// The position that the target entity will be ordered to move towards.
		/// </summary>
		public FixedVector2 TargetPosition { get; private set; }

		public MoveCommand(uint targetID, int playerID, FixedVector2 targetPosition)
			: base(targetID, playerID)
		{
			TargetPosition = targetPosition;
		}

		internal override void Apply()
		{

			var unit = EntityContainer[TargetID] as Unit;

			if(unit == null)
				throw new Exception("Expected a Unit as the target for MoveCommand, got " + EntityContainer[TargetID] + " instead.");

			unit.Transform.Position = TargetPosition;

		}

	}

}
