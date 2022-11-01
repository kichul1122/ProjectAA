using UniRx;
using UnityEngine;

namespace AA.Server
{
	public class Character
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

		public void From(DB.Character data)
		{
			Seq = data.Seq;
			ClickCount.Value = data.ClickCount;
			Level.Value = data.Level;
			Grade = data.Grade;
		}


		public DB.Character To()
		{
			return new DB.Character
			{
				Seq = Seq,
				ClickCount = ClickCount.Value,
				Level = Level.Value,
				Grade = Grade
			};
		}
	}
}
