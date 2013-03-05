/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/


namespace EmpireSharp.Game.Modules.MonoGame
{
	public static class DataExtensions
	{

		public static Microsoft.Xna.Framework.Rectangle ToRectangle(this Data.SpriteMap.SpriteRect rect)
		{

			return new Microsoft.Xna.Framework.Rectangle(rect.X, rect.Y, rect.Width, rect.Height);

		}

	}
}
