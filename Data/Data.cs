namespace AA
{
	public class Data
	{
		public StartUpData StartUp = new StartUpData();

		public CharacterDatas Character = new CharacterDatas();
		//public ItemDatas Item { get; set; } = new ItemDatas();

		public void Dispose()
		{
			Character?.Dispose();
		}
	}
}
