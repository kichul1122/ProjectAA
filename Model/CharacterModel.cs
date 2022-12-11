using System;


namespace AA
{
	[System.Serializable]
	public class CharacterModel : IDisposable
	{
		public CharacterClientData Client;

		public CharacterServerData Server;

		public CharacterMetaData Meta;

		public string Seq => Server.Seq;

		public void Dispose()
		{
			Server?.Dispose();
		}

		public CharacterModel(CharacterServerData server, CharacterMetaData meta)
		{
			Server = server;
			Meta = meta;
			Client = new CharacterClientData();
		}

		//private CharacterData() { }

		//public class Factory : AAFactory<CharacterData>
		//{
		//    public static CharacterData Create(Server.Character server, Meta.Character meta)
		//    {
		//        CharacterData data = Create();
		//        data.Server = server;
		//        data.Meta = meta;
		//        data.Client = new Client.Character();

		//        return data;
		//    }
		//}
	}
}
