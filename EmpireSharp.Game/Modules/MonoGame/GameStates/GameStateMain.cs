/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

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

		[Inject]
		public GameStateMain(IContentService content, IKernel ioc)
		{

			_simulation = new Root();
			_simulation.Init(content.Database);

			_terrainRenderer = ioc.Get<TerrainRenderer>();
			_terrainRenderer.Init(_simulation.Terrain);

			_camera = new Camera();
			_camera.Scale = 62;
			_camera.Rebuild();

		}

		private bool _prevPressed;

		public void Update(float dt)
		{

			var mouseState = Mouse.GetState();
			var keyState = Keyboard.GetState();

			if (mouseState.LeftButton == ButtonState.Pressed) {

				if (!_prevPressed) {


					var simPoint = _camera.TransformViewToSimulation(new Vector2(mouseState.X, mouseState.Y));

					_simulation.QueueCommand(new MoveCommand(1, 0, new FixedVector2((Fix16)simPoint.X, (Fix16)simPoint.Y)));
					_prevPressed = true;


				}

			} else {

				_prevPressed = false;

			}

			Vector2 cameraMoveDirection = Vector2.Zero;

			const float scrollSpeed = 3;

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

				var simDirection = _camera.TransformViewDirectionToSimulation(cameraMoveDirection);

				_camera.SimulationPosition += simDirection * dt * scrollSpeed;

			}

			_simulation.Tick();

		}

		public void Draw()
		{

			var game = Shell as Shell;

			game.GraphicsDevice.Clear(Color.Black);

			_terrainRenderer.Draw(_camera);

			var entities = _simulation.EntityContainer.Entities;

			game.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

			foreach (var baseEntity in entities) {

				if (baseEntity is Unit) {

					var unit = baseEntity as Unit;

					var pos = new Vector2((float)unit.Transform.Position.X, (float)unit.Transform.Position.Y);

					pos = _camera.TransformSimulationToView(pos);

					game.SpriteBatch.Draw(game.WhitePixelTex,
					                      new Rectangle((int) pos.X - 1,
					                                    (int) pos.Y - 1, 2, 2), new Rectangle(0, 0, 1, 1),
					                      Color.Red);

				}
			}

			game.SpriteBatch.End();

		}

	}

}
