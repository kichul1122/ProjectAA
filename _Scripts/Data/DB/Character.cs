using MessagePack;

namespace AA.DB
{
	[MessagePackObject]
	[System.Serializable]
	public class Character
	{
		[Key(0)]
		public string Seq { get; set; }
		[Key(1)]
		public long Id { get; set; }
		[Key(2)]
		public int ClickCount { get; set; }
		[Key(3)]
		public int Level { get; set; }
		[Key(4)]
		public int Grade { get; set; }
	}
}
