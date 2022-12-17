namespace AA
{
	public static class Formula
	{
		public static void Calculate(StatSystem statSystem, EStat eStat, double value)
		{
			statSystem.Get(eStat).Value += value;
		}

		public static double PercentAdd(StatSystem statSystem, EStat eStat, EStat eStatReate)
		{
			return statSystem.GetValue(eStat) * (1d + statSystem.GetValue(eStatReate));
		}

		public static double Get(StatSystem statSystem, EStatAttribute eStatAttribute) => eStatAttribute switch
		{
			EStatAttribute.Attack => PercentAdd(statSystem, EStat.Attack, EStat.AttackPercent),
			_ => default
		};
	}
}
