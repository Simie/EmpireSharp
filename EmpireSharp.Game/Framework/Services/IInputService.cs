/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using System;
using Microsoft.Xna.Framework.Input;

namespace EmpireSharp.Game.Framework.Services
{

	public sealed class KeyboardEventArgs : EventArgs
	{

		public readonly Keys Key;
		public readonly KeyState NewState;

		public KeyboardEventArgs(Keys key, KeyState state)
		{
			Key = key;
			NewState = state;
		}

	}

	public interface IInputService
	{

		event EventHandler<KeyboardEventArgs> KeyPressed; 

		event EventHandler<KeyboardEventArgs> KeyReleased;

		/// <summary>
		/// Amount the mouse wheel has moved since the last frame.
		/// </summary>
		int MouseWheelDelta { get; }

		/// <summary>
		/// Was the key pressed during this update.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		bool IsKeyPressed(Microsoft.Xna.Framework.Input.Keys key);

		/// <summary>
		/// Was the key released during this update.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		bool IsKeyReleased(Microsoft.Xna.Framework.Input.Keys key);
		
		/// <summary>
		/// Is the key currently down
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		bool IsKeyDown(Microsoft.Xna.Framework.Input.Keys key);

		/// <summary>
		/// Is they key current up
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		bool IsKeyUp(Microsoft.Xna.Framework.Input.Keys key);

	}
}