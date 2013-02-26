/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using System;
using System.IO;
using EmpireSharp.Game.Framework.Services;
using EmpireSharp.Game.Modules.MonoGame.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Ninject;

namespace EmpireSharp.Game.Modules.MonoGame
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Shell : Microsoft.Xna.Framework.Game, IShell
	{

		public string WindowTitle { get { return Window.Title; } set { Window.Title = value; } }

		public int Width { get { return Window.ClientBounds.Width; } set { throw new NotImplementedException(); } }
		public int Height { get { return Window.ClientBounds.Height; } set { throw new NotImplementedException(); } }

		private GraphicsDeviceManager _graphics;

		public SpriteBatch SpriteBatch { get; private set; }

		public Texture2D WhitePixelTex;

		internal GameStates.GameStateMain MainGameState { get; set; }

		public Ninject.IKernel IoC { get; private set; }

		private InputManager _inputManager;

		[Inject]
		public Shell(IKernel ioc)
			: base()
		{

			IoC = ioc;

			IoC.Bind<ILog>().To<LogService>().InSingletonScope();
			IoC.Bind<IContentService>().To<ContentService>().InSingletonScope();
			IoC.Bind<IInputService>().To<InputManager>().InSingletonScope();

			_inputManager = IoC.Get<IInputService>() as InputManager;

			_graphics = new GraphicsDeviceManager(this);
			_graphics.PreferredBackBufferHeight = 720;
			_graphics.PreferredBackBufferWidth = 1280;

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


			var contentService = (ContentService) IoC.Get<IContentService>();
			contentService.ContentDirectory = Path.GetFullPath("../../../../Data"); // HACK. TODO: Not hard code this

			contentService.LoadDatabase();

			SpriteBatch = new SpriteBatch(GraphicsDevice);

			IsMouseVisible = true;

			this.Window.Title = "EmpireSharp";
			 
			MainGameState = IoC.Get<GameStateMain>();

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

			_inputManager.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

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
