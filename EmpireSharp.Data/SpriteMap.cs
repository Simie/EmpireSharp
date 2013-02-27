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

			public string ID { get; set; }

			public SpriteRect Rect { get; set; }

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

	}

}
