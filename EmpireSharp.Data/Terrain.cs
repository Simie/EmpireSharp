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
using Papyrus.DataTypes;

namespace EmpireSharp.Data
{

	[Record]
	public class Terrain : EmpireRecord
	{

		private RecordReference<SpriteMap> _spriteMap = RecordReference<SpriteMap>.Empty;

		[RecordProperty(1)]
		[Category("Graphics")]
		[Description("Sprite map containing tiles for this terrain.")]
		public RecordReference<SpriteMap> SpriteMap
		{
			get { return _spriteMap; }
			set
			{
				ThrowIfReadOnly("SpriteMap");
				_spriteMap = value;
				RaisePropertyChanged("SpriteMap");
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

	}

}
