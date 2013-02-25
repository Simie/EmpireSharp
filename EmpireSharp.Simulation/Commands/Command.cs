/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using Ninject;

namespace EmpireSharp.Simulation.Commands
{

	public abstract class Command
	{

		/// <summary>
		/// ID of the entity this command is targetting.
		/// </summary>
		public uint TargetID { get; private set; }

		/// <summary>
		/// The ID of the player that triggered this command.
		/// </summary>
		public int PlayerID { get; private set; }

		[Inject]
		protected EntityContainer EntityContainer { get; set; }

		public Command(uint targetID, int playerID)
		{
			TargetID = targetID;
			PlayerID = playerID;
		}

		/// <summary>
		/// Apply this action to the target entity.
		/// </summary>
		internal abstract void Apply();

	}

}
