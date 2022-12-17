namespace AA
{
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
