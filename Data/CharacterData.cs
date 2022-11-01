using System;


namespace AA
{


	[System.Serializable]
	public class CharacterData : IDisposable
	{
		public Client.Character Client;

		public Server.Character Server;

		public Meta.Character Meta;

		public string Seq => Server.Seq;

		public void Dispose()
		{
			Server?.Dispose();
		}

		public CharacterData(Server.Character server, Meta.Character meta)
		{
			Server = server;
			Meta = meta;
			Client = new Client.Character();
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
