/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using EmpireSharp.Simulation.Entities;

namespace EmpireSharp.Simulation.Behaviours
{

	abstract class Behaviour
	{

		public bool IsCompleted { get; private set; }

		/// <summary>
		/// Initialise this behaviour on the target entity.
		/// </summary>
		/// <param name="entity"></param>
		public abstract void Init(BaseEntity entity);

		public abstract void Tick();

		public abstract void Cancel();
		
		/// <summary>
		/// Mark this behaviour as completed.
		/// </summary>
		protected void Complete()
		{
			IsCompleted = true;
		}

	}

}
