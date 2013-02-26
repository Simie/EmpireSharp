using EmpireSharp.Simulation;
using EmpireSharp.Simulation.Commands;
using EmpireSharp.Simulation.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EmpireSharp.Windows.GameStates
{

	public class GameStateMain
	{

		public EmpireWindows Game { get; private set; }

		private Simulation.Root _simulation;


		public GameStateMain(EmpireWindows _game)
		{

			Game = _game;

			_simulation = new Root();
			_simulation.Init();

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

			Game.GraphicsDevice.Clear(Color.Black);


			var entities = _simulation.EntityContainer.Entities;

			Game.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

			foreach (var baseEntity in entities) {

				if (baseEntity is Unit) {

					var unit = baseEntity as Unit;

					Game.SpriteBatch.Draw(Game.WhitePixelTex,
					                      new Rectangle((int) unit.Transform.Position.X - 1,
					                                    (int) unit.Transform.Position.Y - 1, 2, 2), new Rectangle(0, 0, 1, 1),
					                      Color.Red);

				}
			}

			Game.SpriteBatch.End();

		}

	}

}
