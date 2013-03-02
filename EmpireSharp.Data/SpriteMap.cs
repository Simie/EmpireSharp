/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using System.Collections.Generic;
using Papyrus;

namespace EmpireSharp.Data
{

	[Record]
	public class SpriteMap : EmpireRecord
	{

		[SubRecord]
		public struct SpriteRect
		{
			public int X, Y, Width, Height;
		}

		[SubRecord]
		public struct SpriteMapItem
		{

			[RecordProperty(1)]
			public string ID { get; set; }

			[RecordProperty(2)]
			public SpriteRect Rect { get; set; }

		}

		[SubRecord]
		public struct Clip
		{

			[RecordProperty(1)]
			public List<int> Items { get; set; }

			[RecordProperty(2)]
			public float PlaySpeed { get; set; }

			[RecordProperty(3)]
			public float RepeatDelay { get; set; }

		}

		private string _assetPath;

		[RecordProperty(1)]
		[PropertyTools.DataAnnotations.InputFilePath]
		public string AssetPath
		{
			get { return _assetPath; }
			set
			{
				ThrowIfReadOnly("AssetPath");
				_assetPath = value;
				RaisePropertyChanged("AssetPath");
			}
		}

		private List<SpriteMapItem> _items = new List<SpriteMapItem>();

		[RecordProperty(2)]
		public List<SpriteMapItem> Items
		{
			get { return _items; }
		}

		private List<Clip> _clips = new List<Clip>();

		[RecordProperty(3)]
		public List<Clip> Clips { get { return _clips; } } 

	}

}
