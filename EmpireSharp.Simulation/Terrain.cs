﻿/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using System.Collections.Generic;
using EmpireSharp.Simulation.Entities;
using Ninject;
using System.Linq;

namespace EmpireSharp.Simulation
{

	public struct Tile
	{

		/// <summary>
		/// Terrain type ID. Index of the actual tile type located in the Terrain service.
		/// </summary>
		public int TypeID;
		
		/// <summary>
		/// Is this tile passable by units. Be sure to check the terrain type too, this only
		/// refers to if the tile is blocked by a building or wall.
		/// </summary>
		public bool Passable;

		/// <summary>
		/// If this tile is inpassible, this might contain the building that is blocking it.
		/// </summary>
		public Building Blocker;

		/// <summary>
		/// Elevation of this tile.
		/// </summary>
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

		private List<Data.Terrain> _terrainTypes;

		public IList<Data.Terrain> TerrainTypes { get { return _terrainTypes.AsReadOnly(); } }

		[Inject]
		protected DataService Data { get; set; }

		public Terrain()
		{

		}

		public void Init(int size)
		{

			Size = size;
			_tileMap = new Tile[Size, Size];

			var terrainTypes = Data.Database.GetRecordsOfType<Data.Terrain>();

			_terrainTypes = new List<Data.Terrain>(terrainTypes);

			for (int i = 0; i < Size; i++) {

				for (int j = 0; j < Size; j++) {

					_tileMap[i,j] = new Tile() {
						TypeID = 0
					};

				}

			}

		}

		public Tile At(int x, int y)
		{
			return _tileMap[x, y];
		}

		/// <summary>
		/// Mark a tile as filled with a building blocking the area.
		/// </summary>
		/// <returns></returns>
		internal bool MarkTileCollisionFill(Entities.Building building, int x, int y)
		{

			_tileMap[x, y].Passable = false;
			_tileMap[x, y].Blocker = building;

			return true;

		}

	}

}
