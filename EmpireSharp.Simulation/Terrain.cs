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

namespace EmpireSharp.Simulation
{

	public struct Tile
	{

		public int TypeID;
		public bool Passable;
		public int Elevation;

	}

	/// <summary>
	/// It's the map
	/// </summary>
	public class Terrain
	{

		/// <summary>
		/// Internal representation of the terrain
		/// </summary>
		private Tile[,] _tileMap;

		/// <summary>
		/// Width/Height of the terrain
		/// </summary>
		public int Size { get; private set; }

		public Terrain(int size)
		{

			Size = size;
			_tileMap = new Tile[Size,Size];

		}

	}

}
