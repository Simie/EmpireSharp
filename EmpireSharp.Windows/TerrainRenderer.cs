/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using System.Collections.Generic;
using EmpireSharp.Data;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace EmpireSharp.Windows
{

	class TerrainRenderer
	{

		private Simulation.Terrain _terrain;

		private Dictionary<Data.Terrain, Texture2D> _terrainLookup; 

		public TerrainRenderer(Simulation.Terrain terrain)
		{

			_terrain = terrain;

			_terrainLookup = new Dictionary<Terrain, Texture2D>();

			for (int i = 0; i < _terrain.TerrainTypes.Count; i++) {

				var t = _terrain.TerrainTypes[i];
				

			}

		}

		public void Draw(Camera camera)
		{
			
		}


	}

}
