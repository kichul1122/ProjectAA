using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace AA
{
	public class MainScene : MonoBehaviour, IScene
	{
		#region Interface

		public ESceneName Name => ESceneName.Main;

		UniTask IScene.StartAsync()
		{
			throw new System.NotImplementedException();
		}

		async UniTask IScene.DisposeAsync()
		{
			await UniTask.Yield();
		}

		#endregion Interface

		//private Subject<UniRx.Unit> onFinish = new Subject<UniRx.Unit>();

		private void Start()
		{
			StartMainScene().Forget();
		}

		private async UniTaskVoid StartMainScene()
		{
			await SceneManager.LoadAdditiveAsync(EAdditiveSceneName.Manager);
			await SceneManager.LoadAdditiveAsync(EAdditiveSceneName.UI);

			Managers.Scene.SetCurrentScene(this);

			Managers.Scene.ChangeAsync(ESceneName.Patch).Forget();
		}
	}
}