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

		private Texture2D _blank;

		[Inject]
		public IContentService Content { get; private set; }

		[Inject]
		public IShell Shell { get; private set; }
 
		public void Init(Simulation.Terrain terrain)
		{

			_terrain = terrain;

			_blank = Content.GetTexture("Assets/Terrain/blank.png");

			/*_terrainLookup = new List<Texture2D>();

			for (int i = 0; i < _terrain.TerrainTypes.Count; i++) {

				var t = _terrain.TerrainTypes[i];
				_terrainLookup.Add(Content.GetTexture(t.SpriteMap.Value.AssetPath));

			}*/

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

					//var tex = _terrainLookup[tile.TypeID];

					//int tileIndice = ((size - i) % (size - 1)) * (size + j) % size;
					Rectangle? sourceRect = null;// TileSourceRect(tile.TypeID, i, j, ref tex);

					var targetPosition = camera.TransformSimulationToView(new Vector2(i + 0.5f, j + 0.5f));

					sb.Draw(_blank, new Rectangle((int)(targetPosition.X - tileWidth * 0.5f), (int)(targetPosition.Y - tileHeight * 0.5f), tileWidth, tileHeight), sourceRect, Color.White);

				}

			}

			sb.End();

		}

		public Rectangle TileSourceRect(int tileType, int x, int y, ref Texture2D tex)
		{

			var type = _terrain.TerrainTypes[tileType];
			var spriteMap = type.SpriteMap.Value;

			var tileCount = spriteMap.Items.Count;

			

			int indice = ((x & 3) + (y & 3)) % tileCount;

			var s = indice.ToString();
			var tile = spriteMap.Items.FirstOrDefault(p => p.ID == s);

			if (tile.ID == null)
				return Rectangle.Empty;

			return new Rectangle(tile.Rect.X, tile.Rect.Y, tile.Rect.Width, tile.Rect.Height);

		}


	}

}
