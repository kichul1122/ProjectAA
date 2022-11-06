using Cysharp.Threading.Tasks;
using MonsterLove.StateMachine;
using System.Collections;
using UniRx;
using UnityEngine;

namespace AA
{
	public partial class StartUpScene : MonoBehaviour
	{
		IEnumerator LoadPopup_Enter() => UniTask.ToCoroutine(async () =>
		{
			await UniTask.Yield();

			_stateMachine.ChangeState(EStartUpSceneState.AppVersion);
		});

		IEnumerator AppVersion_Enter() => UniTask.ToCoroutine(async () =>
		{
			await UniTask.Yield();

			_stateMachine.ChangeState(EStartUpSceneState.DownloadApp);
		});

		IEnumerator DownloadApp_Enter() => UniTask.ToCoroutine(async () =>
		{
			await UniTask.Yield();

			_stateMachine.ChangeState(EStartUpSceneState.MetaVersion);
		});

		IEnumerator MetaVersion_Enter() => UniTask.ToCoroutine(async () =>
		{
			await UniTask.Yield();

			_stateMachine.ChangeState(EStartUpSceneState.LoadMeta);
		});

		IEnumerator LoadMeta_Enter() => UniTask.ToCoroutine(async () =>
		{
			await UniTask.Yield();

			_stateMachine.ChangeState(EStartUpSceneState.Login);
		});

		IEnumerator Login_Enter() => UniTask.ToCoroutine(async () =>
		{
			await UniTask.Yield();

			bool isFristLogin = true;

			if (isFristLogin)
			{
				_stateMachine.ChangeState(EStartUpSceneState.CreateUserData);
			}
			else
			{
				_stateMachine.ChangeState(EStartUpSceneState.LoadServerData);
			}
		});

		IEnumerator CreateUserData_Enter() => UniTask.ToCoroutine(async () =>
		{
			await UniTask.Yield();

			_stateMachine.ChangeState(EStartUpSceneState.LoadServerData);
		});

		IEnumerator LoadServerData_Enter() => UniTask.ToCoroutine(async () =>
		{
			await UniTask.Yield();

			_stateMachine.ChangeState(EStartUpSceneState.GamePlay);
		});

	}
	public partial class StartUpScene : MonoBehaviour, IScene
	{
		public enum EStartUpSceneState
		{
			LoadStartUpData,
			LoadPopup,
			AppVersion,
			DownloadApp,
			MetaVersion,
			LoadMeta,
			Login,
			CreateUserData,
			LoadServerData,
			GamePlay,

		}

		private StateMachine<EStartUpSceneState, StateEmptyDriver> _stateMachine;

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
			SetUpAsync().Forget();
		}

		private async UniTaskVoid SetUpAsync()
		{
			Managers.Scene.SetCurrentScene(this);

			_stateMachine = new StateMachine<EStartUpSceneState, StateEmptyDriver>(this);

			Observable.FromEvent<EStartUpSceneState>(
					h => _stateMachine.Changed += h, h => _stateMachine.Changed -= h)
				.Subscribe(state => Debug.Log($"State: {state}")).AddTo(this);

			_stateMachine.ChangeState(EStartUpSceneState.LoadStartUpData);

			await UniTask.Yield();
		}
	}
}