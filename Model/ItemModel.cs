using System;

namespace AA
{
	[System.Serializable]
	public class ItemModel : IDisposable
	{
		public string Seq;
		public long Id;

		public StatSystem StatSystem = new();

		public void MergeTo(StatSystem statSystem)
		{
			StatSystem.MergeTo(statSystem);
		}

		public IObservable<StatModifier> OnUpdateStatAsObservable() => StatSystem.OnUpdateAsObservable();

		public void Dispose()
		{
			StatSystem.Dispose();
		}
	}
}
