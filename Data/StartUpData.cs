using System;
using UniRx;

namespace AA
{
	[System.Serializable]
	public class StartUpSceneData : IDisposable
	{
		public ReactiveProperty<EStartUpState> CurrentStartUpStateRP;

		public int AppMajorVersion;
		public int AppMinorVersion;

		public int PatchMajorVersion;
		public int PatchMinorVersion;

		public StartUpSceneData()
		{
			CurrentStartUpStateRP = new ReactiveProperty<EStartUpState>(EStartUpState.LoadStartUpData);
		}

		public void Dispose()
		{
			CurrentStartUpStateRP?.Dispose();
		}

		public void Load()
		{

		}

		public void ChangeState(EStartUpState eStartUpState)
		{
			CurrentStartUpStateRP.Value = eStartUpState;
		}
	}
}
