﻿/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/

using System.ComponentModel;
using Papyrus;
using Papyrus.DataTypes;

namespace EmpireSharp.Data
{

	[Record]
	public class Terrain : EmpireRecord
	{

		private SpriteClipReference _terrainClip = new SpriteClipReference();

		[RecordProperty(1)]
		[Category("Graphics")]
		[Description("Sprite clip containing tiles for this terrain.")]
		public SpriteClipReference TerrainClip
		{
			get { return _terrainClip; }
			set
			{
				ThrowIfReadOnly("TerrainClip");
				_terrainClip = value;
				RaisePropertyChanged("TerrainClip");
			}
		}

		private Color _fallbackColor;

		[RecordProperty(2)]
		[Category("Graphics")]
		public Color FallbackColor
		{
			get { return _fallbackColor; }
			set
			{
				ThrowIfReadOnly("FallbackColor");
				_fallbackColor = value;
				RaisePropertyChanged("FallbackColor");
			}
		}

		private Color _mapColor;

		[RecordProperty(3)]
		[Category("Graphics")]
		public Color MapColor
		{
			get { return _mapColor; }
			set
			{
				ThrowIfReadOnly("MapColor");
				_mapColor = value;
				RaisePropertyChanged("MapColor");
			}
		}

		private int _mapSize;

		[RecordProperty(4)]
		[Category("Graphics")]
		[PropertyTools.DataAnnotations.Spinnable(Minimum = 1)]
		public int MapSize
		{
			get { return _mapSize; }
			set
			{
				ThrowIfReadOnly("MapSize");
				_mapSize = value;
				RaisePropertyChanged("MapSize");
			}
		}

	}

}
