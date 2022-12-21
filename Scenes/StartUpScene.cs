using Cysharp.Threading.Tasks;
using MonsterLove.StateMachine;
using System.Collections;
using UniRx;
using UnityEngine;

namespace AA
{

	public partial class StartUpScene : MonoBehaviour, IScene
	{
		private StartUpSceneModel _startUpModel;

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
			_startUpModel = Managers.Model.StartUp;

			_stateMachine = new StateMachine<EStartUpSceneState, StateEmptyDriver>(this);

			Observable.FromEvent<EStartUpSceneState>(
					h => _stateMachine.Changed += h, h => _stateMachine.Changed -= h)
				.Subscribe(state => Debug.Log($"State: {state}")).AddTo(this);

			_startUpModel.CurrentStartUpStateRP.Subscribe(currentState => _stateMachine.ChangeState(currentState)).AddTo(this);

			await UniTask.Yield();
		}

		#region State

		IEnumerator LoadStartUpData_Enter() => UniTask.ToCoroutine(async () =>
		{
			await UniTask.Yield();

			_stateMachine.ChangeState(EStartUpSceneState.LoadPopup);
		});

		IEnumerator LoadPopup_Enter() => UniTask.ToCoroutine(async () =>
		{
			await UniTask.Yield();

			_startUpModel.ChangeState(EStartUpSceneState.AppVersion);
		});

		IEnumerator AppVersion_Enter() => UniTask.ToCoroutine(async () =>
		{
			await UniTask.Yield();

			_startUpModel.ChangeState(EStartUpSceneState.DownloadApp);
		});

		IEnumerator DownloadApp_Enter() => UniTask.ToCoroutine(async () =>
		{
			await UniTask.Yield();

			_startUpModel.ChangeState(EStartUpSceneState.MetaVersion);
		});

		IEnumerator MetaVersion_Enter() => UniTask.ToCoroutine(async () =>
		{
			await UniTask.Yield();

			_startUpModel.ChangeState(EStartUpSceneState.LoadMeta);
		});

		IEnumerator LoadMeta_Enter() => UniTask.ToCoroutine(async () =>
		{
			try
			{
				await Managers.Meta.LoadAsync(this.GetCancellationTokenOnDestroy());
			}
			catch (System.Exception e)
			{
				Debug.LogWarning(e);

				//Show YesNoPopup e
			}

			_startUpModel.ChangeState(EStartUpSceneState.Login);
		});

		IEnumerator Login_Enter() => UniTask.ToCoroutine(async () =>
		{
			await UniTask.Yield();

			bool isFristLogin = true;

			if (isFristLogin)
			{
				_startUpModel.ChangeState(EStartUpSceneState.CreateNewModel);
			}
			else
			{
				_startUpModel.ChangeState(EStartUpSceneState.LoadModelFromServer);
			}
		});

		IEnumerator LoadModelFromServer_Enter() => UniTask.ToCoroutine(async () =>
		{
			await UniTask.Yield();

			_startUpModel.ChangeState(EStartUpSceneState.GamePlay);
		});

		IEnumerator CreateNewModel_Enter() => UniTask.ToCoroutine(async () =>
		{
			await UniTask.Yield();

			var characterMD = Managers.Meta.Character.FindByCharacterId(AADefine.First.CharacterModelId);
			var characterSD = new CharacterServerData() { Id = AADefine.First.CharacterModelId };
			var characterModel = new CharacterModel(characterSD, characterMD);
			Managers.Model.Character.Add(characterModel);
			Managers.Model.Constructor();

			_startUpModel.ChangeState(EStartUpSceneState.GamePlay);
		});

		IEnumerator GamePlay_Enter() => UniTask.ToCoroutine(async () =>
		{
			await UniTask.Yield();

			Managers.Scene.ChangeAsync(ESceneName.Field).Forget();

		});
		#endregion
	}
}