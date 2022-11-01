using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AA
{
	public class LobbyScene : MonoBehaviour, IScene
	{
		public ESceneName Name => ESceneName.Lobby;

		public async UniTask StartAsync()
		{
			await Managers.Fade.FadeInAsync();

			await UniTask.Yield();

			await Managers.Fade.FadeOutAsync();

			Managers.Scene.ChangeAsync(ESceneName.Field).Forget();
		}

		public async UniTask DisposeAsync()
		{
			await UniTask.Yield();
		}
	}
}