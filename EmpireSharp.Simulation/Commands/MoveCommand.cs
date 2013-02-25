using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

			var unit = EntityContainer[TargetID];

			unit.Transform.Position = TargetPosition;

		}

	}

}
