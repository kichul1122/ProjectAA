using Sirenix.OdinInspector;
using System;


namespace AA
{
	[System.Serializable]
	public class CharacterModel : IDisposable
	{
		public CharacterClientData Client = new();

		public CharacterServerData Server;

		public CharacterMetaData Meta;

		public StatSystem StatSystem = new();

		public string Seq => Server.Seq;

		public void MergeTo(StatSystem statSystem)
		{
			StatSystem.MergeTo(statSystem);
		}

		public IObservable<StatModifier> OnUpdateStatAsObservable() => StatSystem.OnUpdateAsObservable();

		public void Dispose()
		{
			Server?.Dispose();

			StatSystem.Dispose();
		}

		public CharacterModel(CharacterServerData server, CharacterMetaData meta)
		{
			Server = server;
			Meta = meta;

			MergeFrom();
		}

		[Button]
		public void MergeFrom()
		{
			StatSystem.MergeFrom(Meta.StatModifiers);
		}
	}
}
