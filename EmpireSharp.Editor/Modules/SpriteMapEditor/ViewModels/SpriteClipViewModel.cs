/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/
using System.Windows.Media;
using Caliburn.Micro;
using EmpireSharp.Data;

namespace EmpireSharp.Editor.Modules.SpriteMapEditor.ViewModels
{
	class SpriteClipViewModel : Caliburn.Micro.PropertyChangedBase
	{

		private SpriteMap.Clip _clip;
		private SpriteMapRecordViewModel _map;

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

		private Caliburn.Micro.BindableCollection<int> _items;

		public Caliburn.Micro.BindableCollection<int> Items
		{
			get { return _items; }
			set
			{
				_items = value;
				NotifyOfPropertyChange(() => Items);
			}
		}

		public SpriteClipViewModel(SpriteMap.Clip clip, SpriteMapRecordViewModel map)
		{

			_clip = clip;
			_map = map;

			Items = new BindableCollection<int>(_clip.Items);

		}

	}
}
