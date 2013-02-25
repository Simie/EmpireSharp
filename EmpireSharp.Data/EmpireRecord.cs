using Papyrus;

[assembly: Papyrus.PapyrusModule("8A1666A0-E60D-48B1-BF9E-9A27A6369DDE", typeof(EmpireSharp.Data.EmpireRecord))]

namespace EmpireSharp.Data
{

	[Record(ShowInEditor = false)]
	[ChildRecord(10, typeof(Unit))]
	public class EmpireRecord : Papyrus.DataTypes.Record
	{
	}

}
