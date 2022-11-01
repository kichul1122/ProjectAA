using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace AA
{
	public enum EAdditiveSceneName
	{
		Manager, UI
	}

	public enum ESceneName
	{
		Empty,

		Main,
		Patch,
		Login,
		Lobby,
		Field,
		Dungeon,
	}

	public interface IScene
	{
		ESceneName Name { get; }

		UniTask StartAsync();

		UniTask DisposeAsync();
	}

	public class SceneManager : ManagerMonobehaviour
	{
		public IScene CurrentScene;

		public ESceneName PrevSceneName;

		private const string TagName = "Scene";

		public static async UniTask LoadAdditiveAsync(EAdditiveSceneName eAdditiveSceneName)
		{
			await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(eAdditiveSceneName.ToString(), UnityEngine.SceneManagement.LoadSceneMode.Additive);
		}

		public void SetCurrentScene(IScene currentScene)
		{
			CurrentScene = currentScene;
		}

		private async UniTask LoadAdditiveAsync(ESceneName eSceneName)
		{
			await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(eSceneName.ToString(), UnityEngine.SceneManagement.LoadSceneMode.Additive);
		}

		[Button]
		public async UniTask ChangeAsync(ESceneName eNextSceneName)
		{
			if (CurrentScene != null)
			{
				PrevSceneName = CurrentScene.Name;

				await CurrentScene.DisposeAsync();

				await UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(CurrentScene.Name.ToString());
			}

			await UniTask.Yield();

			await LoadAdditiveAsync(eNextSceneName);

			var scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(eNextSceneName.ToString());
			UnityEngine.SceneManagement.SceneManager.SetActiveScene(scene);

			SetCurrentScene(FindNextScene(eNextSceneName));

			Debug.Log($"Change Scene. {PrevSceneName} To {CurrentScene.Name}");

			CurrentScene.StartAsync().Forget();
		}

		private IScene FindNextScene(ESceneName eNextSceneName)
		{
			var sceneGOs = GameObject.FindGameObjectsWithTag(TagName);

			foreach (var sceneGO in sceneGOs)
			{
				var scene = sceneGO.GetComponent<IScene>();
				if (scene.Name == eNextSceneName)
				{
					return scene;
				}
			}

			throw new Exception("Not Fount FindNextScene");
		}

		public void MoveGameObjectToScene(GameObject gameObject, EAdditiveSceneName eAdditiveSceneName)
		{
			MoveGameObjectToScene(gameObject, eAdditiveSceneName.ToString());
		}

		public void MoveGameObjectToScene(GameObject gameObject, ESceneName eSceneName)
		{
			MoveGameObjectToScene(gameObject, eSceneName.ToString());
		}

		private void MoveGameObjectToScene(GameObject gameObject, string sceneName)
		{
			var scene = UnityEngine.SceneManagement.SceneManager.GetSceneByName(sceneName);
			if (scene == null) return;

			UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(gameObject, scene);
		}

		[Button]
		public void ClearAll()
		{
			UniTask.Create(async () =>
			{
				await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(ESceneName.Empty.ToString());
			}).Forget();

		}

		//[Button]
		//public void ChangeDungeonSceneWithPlayer()
		//{
		//	Managers.Object.MovePlayerCurrentSceneToBridge();

		//	ChangeAsync(ESceneName.Dungeon).Forget();
		//}

		//[Button]
		//public void ChangeFieldSceneWithPlayer()
		//{
		//	Managers.Object.MovePlayerCurrentSceneToBridge();

		//	ChangeAsync(ESceneName.Field).Forget();
		//}
	}
}