using UniRx;

namespace AA
{
	[System.Serializable]
	public class CharacterServerData
	{
		public string Seq;
		public long Id;

		public ReactiveProperty<int> ClickCount = new ReactiveProperty<int>();

		public ReactiveProperty<int> Level = new ReactiveProperty<int>();

		public int Grade;

		public void Dispose()
		{
			ClickCount?.Dispose();
			Level?.Dispose();
		}

		public void From(CharacterDB data)
		{
			Seq = data.Seq;
			ClickCount.Value = data.ClickCount;
			Level.Value = data.Level;
			Grade = data.Grade;
		}


		public CharacterDB To()
		{
			return new CharacterDB
			{
				Seq = Seq,
				ClickCount = ClickCount.Value,
				Level = Level.Value,
				Grade = Grade
			};
		}
	}
}
