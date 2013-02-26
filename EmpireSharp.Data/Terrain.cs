/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using System.ComponentModel;
using Papyrus;

namespace EmpireSharp.Data
{

	[Record]
	public class Terrain : EmpireRecord
	{

		private string _assetPath;

		[RecordProperty(1)]
		[PropertyTools.DataAnnotations.InputFilePath]
		[Category("Graphics")]
		[Description("Path to the asset for this terrain.")]
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

		private int _tileWidth;

		[RecordProperty(2)]
		[Category("Graphics")]
		[PropertyTools.DataAnnotations.Spinnable(Minimum = 1)]
		public int TileWidth
		{
			get { return _tileWidth; }
			set
			{
				ThrowIfReadOnly("TileWidth");
				_tileWidth = value;
				RaisePropertyChanged("TileWidth");
			}
		}

		private int _tileHeight;

		[RecordProperty(3)]
		[Category("Graphics")]
		[PropertyTools.DataAnnotations.Spinnable(Minimum = 1)]
		public int TileHeight
		{
			get { return _tileHeight; }
			set
			{
				ThrowIfReadOnly("TileHeight");
				_tileHeight = value;
				RaisePropertyChanged("TileHeight");
			}
		}

	}

}
