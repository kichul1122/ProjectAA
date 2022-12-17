using UniRx;

namespace AA
{
	[System.Serializable]
	public class StatModel : System.IDisposable
	{
		public CharacterModel characterModel;

		public ItemModels itemModels;

		public StatSystem StatSystem = new();

		private CompositeDisposable _disposables;

		public StatModel() { }

		public StatModel(CharacterModel characterModel, ItemModels itemModels)
		{
			this.characterModel = characterModel;
			this.itemModels = itemModels;

			_disposables = new CompositeDisposable();

			characterModel.MergeTo(StatSystem);
			characterModel.OnUpdateStatAsObservable().Subscribe(statModifier =>
			{
				StatSystem.Update(statModifier);
			}).AddTo(_disposables);

			itemModels.MergeTo(StatSystem);
			itemModels.OnUpdateStatAsObservable().Subscribe(statModifier =>
			{
				StatSystem.Update(statModifier);
			}).AddTo(_disposables);
		}

		public void Dispose()
		{
			_disposables.Dispose();
			StatSystem.Dispose();
		}
	}
}
