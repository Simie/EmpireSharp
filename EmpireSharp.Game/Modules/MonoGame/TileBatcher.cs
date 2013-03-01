/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EmpireSharp.Game.Modules.MonoGame
{
	class TileBatcher
	{
		private const int VertsPerTile = 4;
		private const int IndicesPerTile = 6;

		Matrix _viewMatrix = Matrix.Identity;
		Matrix _projMatrix = Matrix.Identity;

		public readonly GraphicsDevice GraphicsDevice;
		private BasicEffect _effect;

		private VertexPositionColorTexture[] _vertexBuffer;
		short[] _indices;

		private int _vertexPosition;

		private bool _begun;

		private Vector3 _vertexMult = new Vector3(1);

		private int _tileCount;
		private int _drawCalls;

		private Texture2D _activeTexture;

		/// <summary>
		/// Number of tiles drawn during the last Begin/End cycle.
		/// </summary>
		public int TileCount
		{
			get { return _tileCount; }
		}

		/// <summary>
		/// Number of draw calls during the last Begin/End cycle.
		/// </summary>
		public int DrawCallCount
		{
			get { return _drawCalls; }
		}

		public TileBatcher(GraphicsDevice graphics, int batchSize = 600)
		{

			if(graphics == null)
				throw new ArgumentNullException("GraphicsDevice must not be null.");

			GraphicsDevice = graphics;
			//_spriteEffect = new SpriteEffect(GraphicsDevice);

			_effect = new BasicEffect(GraphicsDevice);

			SetBatchSize(batchSize);

		}

		public void SetTileWidth(float width)
		{

			_vertexMult = new Vector3(width, width, 1);

		}

		public void Begin(Matrix viewMatrix, Matrix projMatrix)
		{

			if(_begun)
				throw new InvalidOperationException("Can't begin when you've already begun.");

			_viewMatrix = viewMatrix;
			_projMatrix = projMatrix;
			_begun = true;

			_drawCalls = 0;
			_tileCount = 0;


		}

		public void DrawTile(Texture2D tex, Vector2 pos, Rectangle sourceRect)
		{

			if (_vertexPosition >= _vertexBuffer.Length)
				Flush();

			if(tex != _activeTexture && _activeTexture != null)
				Flush();

			_activeTexture = tex;

			var position = new Vector3(pos, 0);

			int top = _vertexPosition++;
			int right = _vertexPosition++;
			int bottom = _vertexPosition++;
			int left = _vertexPosition++;

			_vertexBuffer[top].Position = position + new Vector3(0f, -0.25f, 0) * _vertexMult;
			_vertexBuffer[right].Position = position + new Vector3(0.5f, 0f, 0) * _vertexMult;
			_vertexBuffer[bottom].Position = position + new Vector3(0f, 0.25f, 0f) * _vertexMult;
			_vertexBuffer[left].Position = position + new Vector3(-0.5f, 0f, 0f) * _vertexMult;

			var topLeft = new Vector2((float)sourceRect.X / tex.Width, (float)sourceRect.Y / tex.Height);
			var botRight = new Vector2(((float) sourceRect.X + sourceRect.Width)/tex.Width,
			                           ((float) sourceRect.Y + sourceRect.Height)/tex.Height);
			
			var size = botRight - topLeft;
			
			_vertexBuffer[top].TextureCoordinate = new Vector2(topLeft.X + size.X * 0.5f, topLeft.Y); 
			_vertexBuffer[right].TextureCoordinate = new Vector2(botRight.X, topLeft.Y + size.Y * 0.5f);
			_vertexBuffer[bottom].TextureCoordinate = new Vector2(topLeft.X + size.X * 0.5f, botRight.Y);
			_vertexBuffer[left].TextureCoordinate = new Vector2(topLeft.X, topLeft.Y + size.Y * 0.5f);

			//var indices = new short[6] { 0, 1, 2, 2, 3, 0 };

			/*_indices[_indicePosition++] = top;
			_indices[_indicePosition++] = right;
			_indices[_indicePosition++] = bottom;
			_indices[_indicePosition++] = bottom;
			_indices[_indicePosition++] = left;
			_indices[_indicePosition++] = top;*/

			++_tileCount;

		}

		public void End()
		{
			
			if(!_begun)
				throw new InvalidOperationException("Can't end when you've not begun.");

			Flush();

			_begun = false;

		}

		private void Flush()
		{

			Setup();

			for (int i = 0; i < _vertexBuffer.Length; i++) {
				_vertexBuffer[i].Color = Color.White;
			}

			FlushVertexArray(0, _vertexPosition);

			GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, _vertexBuffer, 0, _vertexPosition, _indices, 0, _vertexPosition / VertsPerTile);

			_vertexPosition = 0;

		}

		private void Setup()
		{

			_effect.Projection = _projMatrix;
			_effect.View = _viewMatrix;
			_effect.World = Matrix.Identity;

			_effect.TextureEnabled = true;
			_effect.Texture = _activeTexture;

			GraphicsDevice.SamplerStates[0] = SamplerState.LinearClamp;

			_effect.CurrentTechnique.Passes[0].Apply();

		}

		void SetBatchSize(int batchSize)
		{

			_vertexBuffer = new VertexPositionColorTexture[batchSize*VertsPerTile];
			_indices = new short[batchSize*IndicesPerTile];

			for (var i = 0; i < batchSize; i++) {
				_indices[i * 6 + 0] = (short)(i * 4);
				_indices[i * 6 + 1] = (short)(i * 4 + 1);
				_indices[i * 6 + 2] = (short)(i * 4 + 2);
				_indices[i * 6 + 3] = (short)(i * 4 + 2);
				_indices[i * 6 + 4] = (short)(i * 4 + 3);
				_indices[i * 6 + 5] = (short)(i * 4);
			}

		}

		void FlushVertexArray(int start, int end)
		{
			if (start == end)
				return;

			var vertexCount = end - start;

			GraphicsDevice.DrawUserIndexedPrimitives(
				PrimitiveType.TriangleList,
				_vertexBuffer,
				0,
				vertexCount,
				_indices,
				0,
				(vertexCount / 4) * 2,
				VertexPositionColorTexture.VertexDeclaration);

			++_drawCalls;

		}


	}
}
