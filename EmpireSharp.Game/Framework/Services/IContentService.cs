/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using Microsoft.Xna.Framework.Graphics;
using Papyrus;

namespace EmpireSharp.Game.Framework.Services
{
	public interface IContentService
	{

		RecordDatabase Database { get; }

		Texture2D GetTexture(string assetPath);

	}
}