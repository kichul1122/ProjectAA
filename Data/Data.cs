namespace AA
{
	public class Data
	{
		public StartUpSceneData StartUpScene = new StartUpSceneData();

		public FieldSceneData FieldScene = new FieldSceneData();

		public CharacterDatas Character = new CharacterDatas();
		//public ItemDatas Item { get; set; } = new ItemDatas();

		public void Dispose()
		{
			StartUpScene?.Dispose();
			Character?.Dispose();
		}


	}
}
