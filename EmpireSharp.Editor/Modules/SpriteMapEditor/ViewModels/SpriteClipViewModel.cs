/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/
using System.Windows.Media;
using EmpireSharp.Data;

namespace EmpireSharp.Editor.Modules.SpriteMapEditor.ViewModels
{
	class SpriteClipViewModel : Caliburn.Micro.PropertyChangedBase
	{

		private SpriteMap.Clip _clip;

		private int _activeItem;

		public int ActiveItem
		{
			get { return _activeItem; }
			set
			{
				_activeItem = value;
				NotifyOfPropertyChange(() => ActiveItem);
			}
		}

		public SpriteClipViewModel(SpriteMap.Clip clip, ImageSource map)
		{

			_clip = clip;

		}

	}
}
