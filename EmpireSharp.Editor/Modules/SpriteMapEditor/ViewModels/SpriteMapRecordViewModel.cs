/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Caliburn.Micro;
using EmpireSharp.Data;
using EmpireSharp.Editor.Properties;
using Papyrus.Studio.Modules.PapyrusManager;

namespace EmpireSharp.Editor.Modules.SpriteMapEditor.ViewModels
{
	class SpriteMapRecordViewModel : Papyrus.Studio.Framework.RecordViewModel<SpriteMap>
	{

		[Import]
		public IPapyrusManager Papyrus { get; private set; }

		public Caliburn.Micro.BindableCollection<SpriteItemViewModel> Items { get; set; }

		public Caliburn.Micro.BindableCollection<SpriteClipViewModel> Clips { get; set; }

		private SpriteItemViewModel _selectedItem;

		public SpriteItemViewModel SelectedItem
		{
			get { return _selectedItem; }
			set
			{
				_selectedItem = value;
				NotifyOfPropertyChange(() => SelectedItem);
			}
		}

		private SpriteClipViewModel _selectedClip;

		public SpriteClipViewModel SelectedClip
		{
			get { return _selectedClip; }
			set
			{
				_selectedClip = value;
				NotifyOfPropertyChange(() => SelectedClip);
			}
		}

		private BitmapSource _mapSource;

		public BitmapSource MapSource
		{
			get { return _mapSource; }
			set
			{
				_mapSource = value;
				NotifyOfPropertyChange(() => MapSource);
			}
		}

		public override void Open(SpriteMap record)
		{

			base.Open(record);

			var dataPath = Papyrus.DataPath;
			var assetPath = Path.GetFullPath(Path.Combine(dataPath, record.AssetPath));
			MapSource = new BitmapImage(new Uri(assetPath, UriKind.Absolute));

			Items = new BindableCollection<SpriteItemViewModel>(record.Items.Select(p => new SpriteItemViewModel(p, MapSource)));
			Clips = new BindableCollection<SpriteClipViewModel>(record.Clips.Select(p => new SpriteClipViewModel(p, MapSource)));


		}

	}
}
