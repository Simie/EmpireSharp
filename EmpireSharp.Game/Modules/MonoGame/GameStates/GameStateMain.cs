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
using EmpireSharp.Game.Framework.Services;
using EmpireSharp.Simulation;
using EmpireSharp.Simulation.Commands;
using EmpireSharp.Simulation.Entities;
using FixMath.NET;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Ninject;
using Papyrus;

namespace EmpireSharp.Game.Modules.MonoGame.GameStates
{

	public class GameStateMain
	{


		private Simulation.Root _simulation;

		private TerrainRenderer _terrainRenderer;

		private Camera _camera;

		[Inject]
		IContentService Content { get; set; }

		[Inject]
		IShell Shell { get; set; }

		[Inject]
		IInputService Input { get; set; }

		private SpriteContainer _spriteContainer;

		private Vector2 _mouseSimPos;

		private float _targetZoom;

		Dictionary<BaseEntity,Sprite> _sprites = new Dictionary<BaseEntity, Sprite>();
			
		[Inject]
		public GameStateMain(IContentService content, IKernel ioc)
		{

			_simulation = new Root();
			_simulation.Init(content.Database);

			_terrainRenderer = ioc.Get<TerrainRenderer>();
			_terrainRenderer.Init(_simulation.Terrain);

			// HACK: TODO: Inject graphics device in a less broken way
			_spriteContainer = new SpriteContainer(_terrainRenderer.TileBatch.GraphicsDevice);
			ioc.Inject(_spriteContainer);

			_camera = new Camera();
			_camera.Zoom = 1;
			_targetZoom = 1;
			_camera.Rebuild();

		}

		private bool _prevPressed;

		public void Update(float dt)
		{

			_camera.Screen = new Rectangle(0, 0, Shell.Width, Shell.Height);

			var mouseState = Mouse.GetState();
			var keyState = Keyboard.GetState();

			_mouseSimPos = Translate.WorldPointToSimulation(_camera.TransformScreenToWorld(new Vector2(mouseState.X, mouseState.Y)));

			if (mouseState.LeftButton == ButtonState.Pressed) {

				if (!_prevPressed) {


					var simPoint = _mouseSimPos;

					_simulation.QueueCommand(new MoveCommand(1, 0, new FixedVector2((Fix16)simPoint.X, (Fix16)simPoint.Y)));
					_prevPressed = true;


				}

			} else {

				_prevPressed = false;

			}

			Vector2 cameraMoveDirection = Vector2.Zero;

			const float scrollSpeed = 9;

			if (Input.IsKeyDown(Keys.Left)) {
				cameraMoveDirection.X += 1;
			}
			if (Input.IsKeyDown(Keys.Right)) {
				cameraMoveDirection.X -= 1;
			}	
			if (Input.IsKeyDown(Keys.Up)) {
				cameraMoveDirection.Y += 1;
			}
			if (Input.IsKeyDown(Keys.Down)) {
				cameraMoveDirection.Y -= 1;
			}

			if (cameraMoveDirection.LengthSquared() > 0) {

				cameraMoveDirection.Normalize();
				cameraMoveDirection.Y *= 2;

				var simDirection = Translate.WorldDirectionToSimulation(cameraMoveDirection);

				_camera.SimulationPosition += simDirection * dt * scrollSpeed;

			}

			_targetZoom -= Input.MouseWheelDelta * dt * 0.01f;

			_targetZoom = MathHelper.Clamp(_targetZoom, 0.1f, 10f);

			_camera.Zoom -= (_camera.Zoom - _targetZoom)*dt * 6;

			//_camera.Zoom -= 

			_simulation.Tick();

		}

		public void Draw(float dt)
		{

			var game = Shell as Shell;

			game.GraphicsDevice.Clear(Color.Black);

			_terrainRenderer.Draw(_camera);

			_spriteContainer.Draw(_camera, dt);

			var entities = _simulation.EntityContainer.Entities;

			//game.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, null, null, null, _camera.Transform);

			foreach (var baseEntity in entities) {

				if (!_sprites.ContainsKey(baseEntity)) {

					var sprite = new Sprite();
					_sprites[baseEntity] = sprite;

					_spriteContainer.AddSprite(sprite);

				}

				if (baseEntity is Unit) {

					var sprite = _sprites[baseEntity];

					var unit = baseEntity as Unit;

					sprite.SetClip(unit.Data.SpriteClip);
					sprite.SimPosition = unit.Transform.Position.ToVector2();
					sprite.SimRotation = (float)unit.Transform.Rotation;

					/*var pos = new Vector2((float)unit.Transform.Position.X, (float)unit.Transform.Position.Y);

					pos = Translate.SimulationPointToWorld(pos);

					game.SpriteBatch.Draw(game.WhitePixelTex, pos, new Rectangle(0, 0, 16, 32),
					                      Color.Red, 0, new Vector2(8, 30), 1.0f, SpriteEffects.None, 0);*/

				}
			}

			//game.SpriteBatch.End();

			var entity0 = entities[0] as Unit;

			// Draw debug information

			game.SpriteBatch.Begin();

			game.SpriteBatch.DrawString(Content.DebugFont, string.Format("Camera Pos: {0}", _camera.SimulationPosition.ShortString()), new Vector2(10, 10), Color.White);
			game.SpriteBatch.DrawString(Content.DebugFont, string.Format("Mouse Pos: {0}", _mouseSimPos.ShortString()), new Vector2(300, 10), Color.White);
			game.SpriteBatch.DrawString(Content.DebugFont, string.Format("Zoom: {0}", _camera.Zoom.ToString("0.00")), new Vector2(620, 10), Color.White);
			game.SpriteBatch.DrawString(Content.DebugFont, string.Format("Entity0 Pos: {0}", entity0.Transform.Position.ShortString()), new Vector2(10, 30), Color.White);
			game.SpriteBatch.DrawString(Content.DebugFont, string.Format("Entity0: {0}", entity0.Transform.Rotation), new Vector2(10, 50), Color.White);
			game.SpriteBatch.DrawString(Content.DebugFont, string.Format("Terrain [C: {0}, D: {1}]", _terrainRenderer.TileBatch.TileCount, _terrainRenderer.TileBatch.DrawCallCount), new Vector2(300, 30), Color.White);
			game.SpriteBatch.DrawString(Content.DebugFont, string.Format("Sprites [C: {0}, D: {1}]", _spriteContainer.SpriteRenderCount, -1), new Vector2(620, 30), Color.White);

			game.SpriteBatch.End();

		}

	}

}
