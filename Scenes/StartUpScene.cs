using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AA
{
	public class StartUpScene : MonoBehaviour, IScene
	{
		#region Interface
		public ESceneName Name => ESceneName.StartUp;

		UniTask IScene.StartAsync()
		{
			throw new System.NotImplementedException();
		}

		async UniTask IScene.DisposeAsync()
		{
			await UniTask.Yield();
		}

		#endregion Interface

		private void Start()
		{
			StartUpAsync().Forget();
		}

		private async UniTaskVoid StartUpAsync()
		{
			Managers.Scene.SetCurrentScene(this);

			await UniTask.Yield();
		}
	}
}