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

		private GraphicsDevice _graphicsDevice;
 
		public void Init(Simulation.Terrain terrain)
		{

			_terrain = terrain;

			_blank = Content.GetTexture("Assets/Terrain/blank.png");

			_terrainLookup = new List<Texture2D>();

			for (int i = 0; i < _terrain.TerrainTypes.Count; i++) {

				var t = _terrain.TerrainTypes[i];
				_terrainLookup.Add(Content.GetTexture(t.SpriteMap.Value.AssetPath));

			}

			_graphicsDevice = _blank.GraphicsDevice;


		}

		public void Draw(Camera camera)
		{

			const int tileWidth = 97;
			const int tileHeight = 49;

			var sb = ((Shell) Shell).SpriteBatch;

			sb.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, SamplerState.LinearClamp, null, null, null, camera.Transform);

			var size = _terrain.Size;

			for (int i = 0; i < _terrain.Size; i++) {

				for (int j = 0; j < _terrain.Size; j++) {

					var tile = _terrain.At(i, j);
					var type = _terrain.TerrainTypes[tile.TypeID];

					var color = Color.FromNonPremultiplied(100 + (i*50)%155, 100 + (j*50)%155, 255, 255);

					int indice = 0;

					Rectangle? sourceRect = TileSourceRect(ref type, i, j, out indice);

					var targetPosition = Translate.SimulationPointToWorld(new Vector2(i, j));

					sb.Draw(_terrainLookup[tile.TypeID], targetPosition, sourceRect, color, 0, new Vector2(tileWidth, 0) * 0.5f, 1, SpriteEffects.None, 0.1f);

					//sb.DrawString(Content.DebugFont, indice.ToString(), new Vector2(targetPosition.X, targetPosition.Y), Color.Black, 0, new Vector2(12, 12), 1, SpriteEffects.None, 1);

				}

			}

			sb.End();

		}

		public Rectangle TileSourceRect(ref EmpireSharp.Data.Terrain type, int x, int y, out int indice)
		{

			var spriteMap = type.SpriteMap.Value;

			var i = type.MapSize;

			var dx = x%i;
			var dy = y%i;

			indice = dy + dx * i;
			//indice = 0;

			var s = indice.ToString();
			var tile = spriteMap.Items.FirstOrDefault(p => p.ID == s);

			if (tile.ID == null)
				return Rectangle.Empty;

			return new Rectangle(tile.Rect.X, tile.Rect.Y, tile.Rect.Width, tile.Rect.Height);

		}


	}

}
