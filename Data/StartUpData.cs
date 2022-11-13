using System;
using UniRx;

namespace AA
{
	[System.Serializable]
	public class StartUpSceneData : IDisposable
	{
		public ReactiveProperty<EStartUpSceneState> CurrentStartUpStateRP;

		public int AppMajorVersion;
		public int AppMinorVersion;

		public int PatchMajorVersion;
		public int PatchMinorVersion;

		public StartUpSceneData()
		{
			CurrentStartUpStateRP = new ReactiveProperty<EStartUpSceneState>(EStartUpSceneState.LoadStartUpData);
		}

		public void Dispose()
		{
			CurrentStartUpStateRP?.Dispose();
		}

		public void Load()
		{

		}

		public void ChangeState(EStartUpSceneState eStartUpState)
		{
			CurrentStartUpStateRP.Value = eStartUpState;
		}
	}
}
