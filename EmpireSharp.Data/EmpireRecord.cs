/*
*  This Source Code Form is subject to the terms of the Mozilla Public
*  License, v. 2.0. If a copy of the MPL was not distributed with this
*  file, You can obtain one at http://mozilla.org/MPL/2.0/.
*
*  EmpireSharp (c) Simon Moles 2013 (www.simonmoles.com)
*
*/
using Papyrus;

[assembly: Papyrus.PapyrusModule("8A1666A0-E60D-48B1-BF9E-9A27A6369DDE", typeof(EmpireSharp.Data.EmpireRecord))]

namespace EmpireSharp.Data
{

	[Record(ShowInEditor = false)]
	[ChildRecord(10, typeof(Unit))]
	[ChildRecord(11, typeof(Terrain))]
	[ChildRecord(12, typeof(SpriteMap))]
	public class EmpireRecord : Papyrus.DataTypes.Record
	{
	}

}
