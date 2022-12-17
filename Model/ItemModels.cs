using System;
using System.Collections.Generic;
using UniRx;

namespace AA
{
	[System.Serializable]
	public class ItemModels : IDisposable
	{
		public List<ItemModel> Models = new();

		public StatSystem StatSystem = new();

		public void Add(ItemModel model)
		{
			if (Models.FindIndex(_ => _.Seq == model.Seq) == -1) return;

			Models.Add(model);

			model.MergeTo(StatSystem);
			model.OnUpdateStatAsObservable().Subscribe(statModifier =>
			{
				StatSystem.Update(statModifier);
			});
		}

		public void Remove(long id)
		{
			Remove(itemModel => itemModel.Id == id);
		}

		public void Remove(Predicate<ItemModel> predicate)
		{
			for (int i = Models.Count - 1; i >= 0; i--)
			{
				if (predicate(Models[i]))
				{
					Models[i].Dispose();
					Models.RemoveAt(i);
				}
			}
		}

		public void MergeTo(StatSystem statSystem)
		{
			StatSystem.MergeTo(statSystem);
		}

		public void Dispose()
		{
			Models.ForEach(_ => _.Dispose());
			Models.Clear();
		}

		public IObservable<StatModifier> OnUpdateStatAsObservable() => StatSystem.OnUpdateAsObservable();
	}
}
