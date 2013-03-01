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
		private Texture2D _test;

		[Inject]
		public IContentService Content { get; private set; }

		[Inject]
		public IShell Shell { get; private set; }

		private GraphicsDevice _graphicsDevice;

		public TileBatcher TileBatch;


		public void Init(Simulation.Terrain terrain)
		{

			_terrain = terrain;

			_blank = Content.GetTexture("Assets/Terrain/blank.png");

			_test = Content.GetTexture("Assets/Terrain/test.png");
			_terrainLookup = new List<Texture2D>();

			for (int i = 0; i < _terrain.TerrainTypes.Count; i++) {

				var t = _terrain.TerrainTypes[i];
				_terrainLookup.Add(Content.GetTexture(t.SpriteMap.Value.AssetPath));

			}

			_graphicsDevice = _blank.GraphicsDevice;
	
			TileBatch = new TileBatcher(_blank.GraphicsDevice, _test);

			TileBatch.SetTileWidth(97);

		}

		public void Draw(Camera camera)
		{

			var proj = Matrix.CreateOrthographicOffCenter
				(0, _graphicsDevice.Viewport.Width,
				_graphicsDevice.Viewport.Height, 0,
				0, 1);

			TileBatch.Begin(camera.Transform, proj);

			var size = _terrain.Size;

			for (int i = 0; i < size; i++) {

				for (int j = 0; j < size; j++) {

					DrawTile(i, j);

				}

			}
			
			TileBatch.End();

		}


		private void DrawTile(int x, int y)
		{

			var tile = _terrain.At(x, y);
			var type = _terrain.TerrainTypes[tile.TypeID];

			var color = Color.FromNonPremultiplied(100 + (x * 50) % 155, 100 + (y * 50) % 155, 255, 255);

			int indice = 0;

			Rectangle? sourceRect = TileSourceRect(ref type, x, y, out indice);

			var targetPosition = Translate.SimulationPointToWorld(new Vector2(x + 0.5f, y + 0.5f));

			//_primitiveBatch.Begin(PrimitiveType.TriangleList);

			TileBatch.DrawTile(null, targetPosition, Rectangle.Empty);


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
