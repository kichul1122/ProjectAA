namespace AA
{
	public class Model
	{
		public StartUpSceneModel StartUpScene = new();

		public FieldSceneModel FieldScene = new();

		public CharacterModels Character = new();

		public ItemModels Item = new();

		public PlayerStatModel Stat;

		public void Dispose()
		{
			StartUpScene.Dispose();
			//FieldScene.Dispose();
			Character.Dispose();
			Item.Dispose();
			Stat?.Dispose();
		}

		public void SetStatModel(PlayerStatModel statModel)
		{
			Stat = statModel;
		}
	}
}