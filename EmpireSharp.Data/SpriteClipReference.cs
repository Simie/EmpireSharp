/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/
using Papyrus;

namespace EmpireSharp.Data
{

	[SubRecord]
	public class SpriteClipReference
	{

		private RecordReference<SpriteMap> _spriteMap = RecordReference<SpriteMap>.Empty;

		[RecordProperty(1)]
		public RecordReference<SpriteMap> SpriteMap
		{
			get { return _spriteMap; }
			set
			{
				_spriteMap = value;
			}
		}

		private string _clipKey = "";

		[RecordProperty(2)]
		public string ClipKey
		{
			get { return _clipKey; }
			set
			{
				_clipKey = value;
			}
		}
		 
	}

}
