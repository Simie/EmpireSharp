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
using EmpireSharp.Game.Framework.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ninject;

namespace EmpireSharp.Game.Modules.MonoGame
{

	class TerrainRenderer
	{

		private Simulation.Terrain _terrain;

		private List<Texture2D> _terrainLookup;

		[Inject]
		public IContentService Content { get; private set; }

		[Inject]
		public IShell Shell { get; private set; }
 
		public void Init(Simulation.Terrain terrain)
		{

			_terrain = terrain;

			_terrainLookup = new List<Texture2D>();

			for (int i = 0; i < _terrain.TerrainTypes.Count; i++) {

				var t = _terrain.TerrainTypes[i];
				_terrainLookup.Add(Content.GetTexture(t.AssetPath));

			}

		}

		public void Draw(Camera camera)
		{

			const int tileWidth = 97;
			const int tileHeight = 49;

			var sb = ((Shell) Shell).SpriteBatch;

			sb.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, null, null, null, Matrix.Identity);

			var size = _terrain.Size;

			for (int i = 0; i < _terrain.Size; i++) {

				for (int j = 0; j < _terrain.Size; j++) {

					var tile = _terrain.At(i, j);

					var tex = _terrainLookup[tile.TypeID];

					int indice = ((size - i) % (size - 1)) * (size + j) % size;
					var sourceRect = TileSourceRect(tile.TypeID, indice, ref tex);

					sb.Draw(tex, new Rectangle(i*tileWidth, j*tileWidth, tileWidth, tileHeight), sourceRect, Color.White);

				}

			}

			sb.End();

		}

		public Rectangle TileSourceRect(int tileType, int index, ref Texture2D tex)
		{

			var type = _terrain.TerrainTypes[tileType];

			var x = type.TileWidth * index;
			var y = 0;

			while (x >= tex.Width) {
				x -= tex.Width;
				y += type.TileHeight;
			}

			return new Rectangle(x,y,type.TileWidth, type.TileHeight);

		}


	}

}
