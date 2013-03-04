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
using Microsoft.Xna.Framework.Input;
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

		[Inject]
		public IInputService Input { get; private set; }

		private GraphicsDevice _graphicsDevice;

		public TileBatcher TileBatch;
		PrimitiveBatch _debugBatch;
		


		private Rectangle _cullingRect;

		public void Init(Simulation.Terrain terrain)
		{

			_terrain = terrain;

			_blank = Content.GetTexture("Assets/Terrain/blank.png");

			_test = Content.GetTexture("Assets/Terrain/test.png");
			_terrainLookup = new List<Texture2D>();

			for (int i = 0; i < _terrain.TerrainTypes.Count; i++) {

				var t = _terrain.TerrainTypes[i];
				_terrainLookup.Add(Content.GetTexture(t.TerrainClip.SpriteMap.Value.AssetPath));

			}

			_graphicsDevice = _blank.GraphicsDevice;
	
			TileBatch = new TileBatcher(_blank.GraphicsDevice);
			_debugBatch = new PrimitiveBatch(_blank.GraphicsDevice, 16);

			TileBatch.SetTileWidth(97);

		}

		public void Draw(Camera camera)
		{

			var proj = Matrix.CreateOrthographicOffCenter
				(0, _graphicsDevice.Viewport.Width,
				_graphicsDevice.Viewport.Height, 0,
				0, 1);

			// For culling, ignore camera zoom.
			var zoom = camera.Zoom;
			camera.Zoom = 1;

			var topLeft = camera.TransformScreenToWorld(new Vector2(0,0));
			var bottomRight = camera.TransformScreenToWorld(new Vector2(_graphicsDevice.Viewport.Width, _graphicsDevice.Viewport.Height));

			camera.Zoom = zoom;

			var cullSize = bottomRight - topLeft;
			var inflateSize = new Vector2(97, 41);

			_cullingRect = new Rectangle((int)topLeft.X, (int)topLeft.Y, (int)cullSize.X, (int)cullSize.Y);
			_cullingRect.Inflate((int)inflateSize.X, (int)inflateSize.Y);

			TileBatch.Begin(camera.Transform, proj);

			var size = _terrain.Size;

			for (int i = 0; i < size; i++) {

				for (int j = 0; j < size; j++) {

					DrawTile(i, j);

				}

			}
			
			TileBatch.End();

			// Debug Culling Rect
			{

				var cullTopLeft = camera.TransformWorldtoScreen(new Vector2(_cullingRect.Left, _cullingRect.Top));
				var cullBottomRight = camera.TransformWorldtoScreen(new Vector2(_cullingRect.Right, _cullingRect.Bottom));

				var cullTopRight = new Vector2(cullBottomRight.X, cullTopLeft.Y);
				var cullBottomLeft = new Vector2(cullTopLeft.X, cullBottomRight.Y);

				_debugBatch.Begin(proj, Matrix.Identity);

				BasicShapes.DrawPolygon(_debugBatch, new Vector2[4] {cullTopLeft, cullTopRight, cullBottomRight, cullBottomLeft}, 4, Color.Blue);

				var viewTopLeft = camera.TransformWorldtoScreen(topLeft);
				var viewBotRight = camera.TransformWorldtoScreen(bottomRight);

				var viewTopRight = new Vector2(viewBotRight.X, viewTopLeft.Y);
				var viewBotLeft = new Vector2(viewTopLeft.X, viewBotRight.Y);

				BasicShapes.DrawPolygon(_debugBatch, new Vector2[4] {viewTopLeft, viewTopRight, viewBotRight, viewBotLeft}, 4, Color.Red);


				_debugBatch.End();


			}

			if (!Input.IsKeyDown(Keys.LeftShift))
				return;

			var spriteBatch = ((Shell)Shell).SpriteBatch;

			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, null, null, null, camera.Transform);

			for (int i = 0; i < size; i++) {

				for (int j = 0; j < size; j++) {

					DrawTileDebug(i, j, spriteBatch);

				}

			}

			spriteBatch.End();


		}


		private void DrawTile(int x, int y)
		{

			var targetPosition = Translate.SimulationPointToWorld(new Vector2(x + 0.5f, y + 0.5f));

			if(!_cullingRect.Contains((int)targetPosition.X, (int)targetPosition.Y))
				return;

			var tile = _terrain.At(x, y);
			var type = _terrain.TerrainTypes[tile.TypeID];

			var color = Color.FromNonPremultiplied(100 + (x * 50) % 155, 100 + (y * 50) % 155, 255, 255);

			int indice = 0;

			Rectangle? sourceRect = TileSourceRect(ref type, x, y, out indice);

			TileBatch.DrawTile(_terrainLookup[tile.TypeID], targetPosition, sourceRect.Value);


		}

		private void DrawTileDebug(int x, int y, SpriteBatch batch)
		{

			var targetPosition = Translate.SimulationPointToWorld(new Vector2(x + 0.5f, y + 0.5f));

			if(!_cullingRect.Contains((int)targetPosition.X, (int)targetPosition.Y))
				return;

			var tile = _terrain.At(x, y);
			var type = _terrain.TerrainTypes[tile.TypeID];

			var color = Color.FromNonPremultiplied(100 + (x * 50) % 155, 100 + (y * 50) % 155, 255, 255);

			int indice = TileIndice(x,y, type.MapSize);

			var debug = x + ", " + y + "\n" + indice.ToString();
			batch.DrawString(Content.DebugFont, debug, targetPosition, Color.White, 0, Content.DebugFont.MeasureString(debug) * 0.5f, 0.8f, SpriteEffects.None, 0);

		}

		public Rectangle TileSourceRect(ref EmpireSharp.Data.Terrain type, int x, int y, out int indice)
		{

			var terrain = type;
			var spriteMap = type.TerrainClip.SpriteMap.Value;

			indice = TileIndice(x, y, type.MapSize);


			var clip = spriteMap.Clips.FirstOrDefault(p => p.Key == terrain.TerrainClip.ClipKey);

			var tile = spriteMap.Items[clip.Items[indice]];

			return new Rectangle(tile.Rect.X, tile.Rect.Y, tile.Rect.Width, tile.Rect.Height);

		}

#if NET45
		[System.Runtime.CompilerServices.MethodImplAttribute(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
#endif
		public static int TileIndice(int x, int y, int mapSize)
		{

			y = mapSize - (y%mapSize); // AOE2 used opposite y axis

			return ((x % mapSize) + (y % mapSize) * mapSize);

		}


	}

}
