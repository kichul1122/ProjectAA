namespace AA
{
	public class Model
	{
		public StartUpSceneModel StartUpScene = new StartUpSceneModel();

		public FieldSceneModel FieldScene = new FieldSceneModel();

		public CharacterModels Character = new CharacterModels();
		//public ItemDatas Item { get; set; } = new ItemDatas();

		public void Dispose()
		{
			StartUpScene?.Dispose();
			Character?.Dispose();
		}


	}
}
