using MessagePack;

namespace AA
{
	[MessagePackObject(true)]
	[System.Serializable]
	public struct StatModifier
	{
		public EStat EStat;
		public double Value;

		public StatModifier(EStat eStat, double value)
		{
			EStat = eStat;
			Value = value;
		}
	}
}
