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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Ninject;

namespace EmpireSharp.Game.Modules.MonoGame
{

	public class SpriteContainer
	{

		[Inject]
		Framework.Services.IContentService Content { get; set; }

		private LinkedList<Sprite> _sprites;

		private List<Sprite> _culledBatch;
		private SpriteBatch _batch;

		public readonly GraphicsDevice GraphicsDevice;

		private Texture2D _errorPx;

		private Dictionary<SpriteMap, Texture2D> _textureCache; 

		public SpriteContainer(GraphicsDevice graphicsDevice)
		{

			_textureCache = new Dictionary<SpriteMap, Texture2D>();

			_sprites = new LinkedList<Sprite>();
			_culledBatch = new List<Sprite>();
			GraphicsDevice = graphicsDevice;

			_batch = new SpriteBatch(GraphicsDevice);
			_errorPx = new Texture2D(GraphicsDevice,1,1);
			_errorPx.SetData(new Color[] {Color.White});

		}

		public void AddSprite(Sprite sprite)
		{
			_sprites.AddLast(sprite);
		}

		public void RemoveSprite(Sprite sprite)
		{
			_sprites.Remove(sprite);
		}

		public void Draw(Camera camera, float dt)
		{

			var proj = Matrix.CreateOrthographicOffCenter
						(0, GraphicsDevice.Viewport.Width,
						GraphicsDevice.Viewport.Height, 0,
						0, 1);


			foreach (var sprite in _sprites) {

				sprite.Update(dt);

				if(sprite.SpriteMap == null)
					continue;

				if (!_textureCache.ContainsKey(sprite.SpriteMap)) {
					_textureCache[sprite.SpriteMap] = Content.GetTexture(sprite.SpriteMap.AssetPath);
				}

			}

			RefreshVisible(camera);

			_batch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, camera.Transform);

			for (var i = 0; i < _culledBatch.Count; i++) {

				var sprite = _culledBatch[i];

				_batch.Draw(_textureCache[sprite.SpriteMap], Translate.SimulationPointToWorld(sprite.SimPosition), sprite.ActiveItem.Rect.ToRectangle(),
					  Color.Red, Translate.SimulationRotationToWorld(sprite.SimRotation), new Vector2(sprite.ActiveItem.Origin.Width, sprite.ActiveItem.Origin.Height), 1.0f, SpriteEffects.None, 0);
			}

			_batch.End();


		}

		/// <summary>
		/// Update the list of visible sprites
		/// </summary>
		/// <param name="camera"></param>
		private void RefreshVisible(Camera camera)
		{

			var topLeft = camera.TransformScreenToWorld(new Vector2(0, 0));
			var bottomRight = camera.TransformScreenToWorld(new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height)); 
			
			var cullSize = bottomRight - topLeft;

			var cullingRect = new Rectangle((int)topLeft.X, (int)topLeft.Y, (int)cullSize.X, (int)cullSize.Y);
			cullingRect.Inflate(2, 2); // Inflate by 2 in case int rounding has left a 1px gap

			bool result;

			_culledBatch.Clear();

			foreach (var sprite in _sprites) {

				if(sprite.SpriteMap == null)
					continue;

				sprite.WorldRect.Intersects(ref cullingRect, out result);

				if (result)
					_culledBatch.Add(sprite);

			}

		}

	}

}
