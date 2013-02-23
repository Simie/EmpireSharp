/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

#region Using Statements
using System;
using System.Collections.Generic;
using EmpireSharp.Simulation;
using EmpireSharp.Windows.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace EmpireSharp.Windows
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class EmpireWindows : Microsoft.Xna.Framework.Game
	{


		private GraphicsDeviceManager _graphics;

		public SpriteBatch SpriteBatch { get; private set; }

		public Texture2D WhitePixelTex;

		internal GameStates.GameStateMain MainGameState { get; set; }

		public EmpireWindows()
			: base()
		{

			_graphics = new GraphicsDeviceManager(this);
			_graphics.PreferredBackBufferHeight = 720;
			_graphics.PreferredBackBufferWidth = 1280;

			//Content = new ContentManager(Services);
			Content.RootDirectory = "Content";

		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{

			base.Initialize();

			SpriteBatch = new SpriteBatch(GraphicsDevice);

			IsMouseVisible = true;

			this.Window.Title = "EmpireSharp";

			MainGameState = new GameStateMain(this);

			WhitePixelTex = new Texture2D(GraphicsDevice, 1,1);
			WhitePixelTex.SetData(new Color[] {Color.White});
			
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent()
		{

			base.LoadContent();

		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unload
		/// all content.
		/// </summary>
		protected override void UnloadContent()
		{

			base.UnloadContent();

		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			MainGameState.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{

			MainGameState.Draw();
			base.Draw(gameTime);

		}
	}
}
