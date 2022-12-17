using UniRx;

namespace AA
{
	public class FormulaSystem
	{
		private readonly StatSystem _statSystem;

		public FormulaSystem(StatSystem statSystem)
		{
			_statSystem = statSystem;
		}

		public double GetValue(EStatAttribute eStatAttribute)
		{
			return Formula.Get(_statSystem, eStatAttribute);
		}

		public IReadOnlyReactiveProperty<double> AsObservable(EStatAttribute eStatAttribute) => Formula.AsReactiveProperty(_statSystem, eStatAttribute);
	}

	public static class Formula
	{
		public static void Calculate(StatSystem statSystem, EStat eStat, double value)
		{
			statSystem.Get(eStat).Value += value;
		}

		public static double PercentAdd(StatSystem statSystem, EStat eStat, EStat eStatPercent)
		{
			return statSystem.GetValue(eStat) * (1d + statSystem.GetValue(eStatPercent));
		}

		public static double Get(StatSystem statSystem, EStatAttribute eStatAttribute) => eStatAttribute switch
		{
			EStatAttribute.Attack => PercentAdd(statSystem, EStat.Attack, EStat.AttackPercent),
			EStatAttribute.MoveSpeed => PercentAdd(statSystem, EStat.MoveSpeed, EStat.MoveSpeedPercent),
			EStatAttribute.MaxHp => PercentAdd(statSystem, EStat.MaxHp, EStat.MaxHpPercent),
			EStatAttribute.CriticalPercent => statSystem.GetValue(EStat.CriticalPercent),
			EStatAttribute.CriticalDamagePrercent => statSystem.GetValue(EStat.CriticalDamagePrercent),
			_ => default
		};

		public static IReadOnlyReactiveProperty<double> AsReactiveProperty(StatSystem statSystem, EStatAttribute eStatAttribute) => eStatAttribute switch
		{
			EStatAttribute.Attack => Observable.Merge(statSystem.Get(EStat.Attack), statSystem.Get(EStat.AttackPercent)).Select(_ => PercentAdd(statSystem, EStat.Attack, EStat.AttackPercent)).ToReactiveProperty(),
			EStatAttribute.MoveSpeed => Observable.Merge(statSystem.Get(EStat.MoveSpeed), statSystem.Get(EStat.MoveSpeedPercent)).Select(_ => PercentAdd(statSystem, EStat.MoveSpeed, EStat.MoveSpeedPercent)).ToReactiveProperty(),
			EStatAttribute.MaxHp => Observable.Merge(statSystem.Get(EStat.MaxHp), statSystem.Get(EStat.MaxHpPercent)).Select(_ => PercentAdd(statSystem, EStat.MaxHp, EStat.MaxHpPercent)).ToReactiveProperty(),
			EStatAttribute.CriticalPercent => statSystem.Get(EStat.CriticalPercent),
			EStatAttribute.CriticalDamagePrercent => statSystem.Get(EStat.CriticalDamagePrercent),
			_ => default
		};

		//public static IObservable<double> GetAsObservableWithState(StatSystem statSystem, EStatAttribute eStatAttribute) => eStatAttribute switch
		//{
		//	EStatAttribute.Attack => FormulaObservable.GetAsObservable(PercentAdd, statSystem, EStat.Attack, EStat.AttackPercent),
		//	EStatAttribute.MoveSpeed => FormulaObservable.GetAsObservable(PercentAdd, statSystem, EStat.MoveSpeed, EStat.MoveSpeedPercent),
		//	EStatAttribute.MaxHp => FormulaObservable.GetAsObservable(PercentAdd, statSystem, EStat.MaxHp, EStat.MaxHpPercent),
		//	EStatAttribute.CriticalPercent => FormulaObservable.GetAsObservable(statSystem, EStat.CriticalPercent),
		//	EStatAttribute.CriticalDamagePrercent => FormulaObservable.GetAsObservable(statSystem, EStat.CriticalDamagePrercent),
		//	_ => default
		//};
	}
}

//public static class FormulaObservable
//{
//	public static IObservable<double> GetAsObservable(StatSystem statSystem, EStat eStat) => statSystem.Get(eStat);

//	public static IObservable<double> GetAsObservable(Func<StatSystem, EStat, EStat, double> onCalculate, StatSystem statSystem, EStat eStat1, EStat eStat2)
//	{
//		var state = Observable.Merge(statSystem.Get(eStat1), statSystem.Get(eStat2));

//		return Observable.CreateWithState<double, IObservable<double>>(state, (observable, observer) =>
//		{
//			observer.OnNext(onCalculate(statSystem, eStat1, eStat2));
//			return observable.Select(_ => onCalculate(statSystem, eStat1, eStat2)).Subscribe(observer);
//		});
//	}

//	public static IObservable<double> GetAsObservable(Func<StatSystem, EStat, EStat, EStat, double> onCalculate, StatSystem statSystem, EStat eStat1, EStat eStat2, EStat eStat3)
//	{
//		var state = Observable.Merge(statSystem.Get(eStat1), statSystem.Get(eStat2), statSystem.Get(eStat3));

//		return Observable.CreateWithState<double, IObservable<double>>(state, (observable, observer) =>
//		{
//			observer.OnNext(onCalculate(statSystem, eStat1, eStat2, eStat3));
//			return observable.Select(_ => onCalculate(statSystem, eStat1, eStat2, eStat3)).Subscribe(observer);
//		});
//	}
//}