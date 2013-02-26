/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using System.Diagnostics;
using Ninject;

namespace EmpireSharp.Simulation.Entities
{

	public class Building : BaseEntity
	{

		/// <summary>
		/// The tile this buildings origin will be positioned on.
		/// </summary>
		public TilePosition Position { get; private set; }

		[Inject]
		protected Terrain Terrain { get; set; }

		public int Width { get; private set; }
		public int Height { get; private set; }

		public override void Init()
		{

			Debug.Assert(Width > 0, "Width must be greater than zero.");
			Debug.Assert(Height > 0, "Height must be greater than zero.");

			base.Init();

			for (int i = 0; i < Width; i++) {

				for (int j = 0; j < Height; j++) {

					Terrain.MarkTileCollisionFill(this, Position.X + i, Position.Y + j);

				}

			}

		}

	}

}
