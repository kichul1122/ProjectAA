using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace AA
{
	public class StatSystem : IDisposable
	{
		[ShowInInspector]
		public Dictionary<EStat, ReactiveProperty<double>> Stats = new();

		private Subject<StatModifier> _onUpdateSubject;
		public IObservable<StatModifier> OnUpdateAsObservable() => _onUpdateSubject ??= new Subject<StatModifier>();

		private void Update(KeyValuePair<EStat, ReactiveProperty<double>> stat)
		{
			Update(stat.Key, stat.Value.Value);
		}

		public void Update(EStat eStat, double value)
		{
			Formula.Calculate(this, eStat, value);

			_onUpdateSubject?.OnNext(new StatModifier(eStat, value));
		}

		public void Update(StatModifier statModifier)
		{
			Update(statModifier.EStat, statModifier.Value);
		}

		public ReactiveProperty<double> Get(EStat eStat)
		{
			if (Stats.TryGetValue(eStat, out var getValue)) return getValue;

			ReactiveProperty<double> value = new();
			Stats.Add(eStat, value);

			return value;
		}

		public double GetValue(EStat eStat)
		{
			return Get(eStat).Value;
		}

		public void MergeTo(StatSystem statSystem)
		{
			foreach (var stat in Stats)
			{
				statSystem.Update(stat);
			}
		}

		public void Dispose()
		{
			AAHelper.DisposeSubject(ref _onUpdateSubject);
		}

		[Button]
		public void ToJson()
		{
			foreach (var statModifier in Stats)
			{
				Debug.Log($"EStat: {statModifier.Key} Value: {statModifier.Value.Value}");
			}
		}

		public void MergeFrom(IEnumerable<StatModifier> statModifiers)
		{
			statModifiers.ForEach(statModifier => Update(statModifier));
		}
	}
}
