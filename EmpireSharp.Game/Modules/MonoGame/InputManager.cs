/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using System;
using System.Linq;
using EmpireSharp.Game.Framework.Services;
using Microsoft.Xna.Framework.Input;

namespace EmpireSharp.Game.Modules.MonoGame
{
	class InputManager : IInputService
	{

		public event EventHandler<KeyboardEventArgs> KeyPressed;

		public event EventHandler<KeyboardEventArgs> KeyReleased;

		/// <summary>
		/// Keyboard state during this frame.
		/// </summary>
		public KeyboardState KeyboardState { get; private set; }

		/// <summary>
		/// Keyboard state from the last frame.
		/// </summary>
		public KeyboardState PreviousKeyboardState { get; private set; }
	
		/// <summary>
		/// Keyboard state during this frame.
		/// </summary>
		public MouseState MouseState { get; private set; }

		/// <summary>
		/// Keyboard state from the last frame.
		/// </summary>
		public MouseState PreviousMouseState { get; private set; }

		/// <summary>
		/// Amount the mouse wheel has moved since the last frame.
		/// </summary>
		public int MouseWheelDelta { get; private set; }

		public bool IsKeyPressed(Keys key)
		{
			return KeyboardState.IsKeyDown(key) &&  PreviousKeyboardState.IsKeyUp(key);
		}

		public bool IsKeyReleased(Keys key)
		{
			return PreviousKeyboardState.IsKeyDown(key) && KeyboardState.IsKeyUp(key);
		}

		public bool IsKeyDown(Keys key)
		{
			return KeyboardState.IsKeyDown(key);
		}

		public bool IsKeyUp(Keys key)
		{
			return KeyboardState.IsKeyUp(key);
		}

		public void Update(float dt)
		{

			PreviousKeyboardState = KeyboardState;
			KeyboardState = Keyboard.GetState();

			PreviousMouseState = MouseState;
			MouseState = Mouse.GetState();

			MouseWheelDelta = PreviousMouseState.ScrollWheelValue - MouseState.ScrollWheelValue;

			EventUpdate();

		}

		void EventUpdate()
		{

			if (KeyReleased == null && KeyPressed == null)
				return; // Early out if there are no listeners

			var pressedKeys = KeyboardState.GetPressedKeys();
			var prevPressedKeys = PreviousKeyboardState.GetPressedKeys();

			if (KeyPressed != null) {

				foreach (var key in pressedKeys.Where(key => !prevPressedKeys.Contains(key))) {
					OnKeyPressed(key);
				}

			}

			if (KeyReleased != null) {

				foreach (var key in prevPressedKeys.Where(key => !pressedKeys.Contains(key))) {
					OnKeyReleased(key);
				}

			}

		}

		private void OnKeyPressed(Keys key)
		{

			if (KeyPressed != null) {
				KeyPressed(this, new KeyboardEventArgs(key, KeyState.Down));
			}

		}
	
		private void OnKeyReleased(Keys key)
		{

			if (KeyReleased != null) {
				KeyReleased(this, new KeyboardEventArgs(key, KeyState.Up));
			}

		}

	}
}
