/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace EmpireSharp.Windows
{

	/// <summary>
	/// Contains the camera location in simulation-space, and provides transforms to rendering to screen space.
	/// </summary>
	public class Camera
	{

		private Vector2 _simulationPosition;

		public bool IsDirty { get; set; }

		/// <summary>
		/// Position of the camera in simulation space.
		/// </summary>
		public Vector2 SimulationPosition
		{

			get { return _simulationPosition; }
			set { 
				_simulationPosition = value;
				IsDirty = true;
			}

		}



	}

}
