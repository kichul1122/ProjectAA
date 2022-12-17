using System;


namespace AA
{
	[System.Serializable]
	public class CharacterModel : IDisposable
	{
		public CharacterClientData Client;

		public CharacterServerData Server;

		public CharacterMetaData Meta;

		public StatSystem StatSystem;

		public string Seq => Server.Seq;

		public void Dispose()
		{
			Server?.Dispose();
		}

		public CharacterModel(CharacterServerData server, CharacterMetaData meta)
		{
			Server = server;
			Meta = meta;
			Client = new();
			StatSystem = new();
		}
	}
}
