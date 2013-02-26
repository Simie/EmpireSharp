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

		[Inject]
		IContentService Content { get; set; }

		[Inject]
		IShell Shell { get; set; }

		[Inject]
		public GameStateMain(IContentService content, IKernel ioc)
		{

			_simulation = new Root();
			_simulation.Init(content.Database);

			_terrainRenderer = ioc.Get<TerrainRenderer>();
			_terrainRenderer.Init(_simulation.Terrain);

		}

		private bool _prevPressed;

		public void Update(float dt)
		{

			var mouseState = Mouse.GetState();

			if (mouseState.LeftButton == ButtonState.Pressed) {

				if (!_prevPressed) {
					
					_simulation.QueueCommand(new MoveCommand(1, 0, new FixedVector2(0,0)));

				}

			} else {

				_prevPressed = false;

			}

			_simulation.Tick();

		}

		public void Draw()
		{

			var game = Shell as Shell;

			game.GraphicsDevice.Clear(Color.Black);

			_terrainRenderer.Draw(null);

			var entities = _simulation.EntityContainer.Entities;

			game.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

			foreach (var baseEntity in entities) {

				if (baseEntity is Unit) {

					var unit = baseEntity as Unit;

					game.SpriteBatch.Draw(game.WhitePixelTex,
					                      new Rectangle((int) unit.Transform.Position.X - 1,
					                                    (int) unit.Transform.Position.Y - 1, 2, 2), new Rectangle(0, 0, 1, 1),
					                      Color.Red);

				}
			}

			game.SpriteBatch.End();

		}

	}

}
