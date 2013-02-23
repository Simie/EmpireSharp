using EmpireSharp.Simulation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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


		public void Update(float dt)
		{

			_simulation.Tick();

		}

		public void Draw()
		{

			Game.GraphicsDevice.Clear(Color.Black);


			var entities = _simulation.EntityContainer.Entities;

			Game.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

			foreach (var baseEntity in entities) {

				Game.SpriteBatch.Draw(Game.WhitePixelTex, new Rectangle((int)baseEntity.Transform.Position.X - 1, (int)baseEntity.Transform.Position.Y - 1, 2, 2), new Rectangle(0, 0, 1, 1), Color.Red);

			}

			Game.SpriteBatch.End();

		}

	}

}
