using Cysharp.Threading.Tasks;
using MessagePipe;
using UnityEngine;

namespace AA
{
	public class FieldScene : MonoBehaviour, IScene
	{
		private FieldSceneModel _fieldSceneModel;
		private IPublisher<CharacterFollow.TargetTransformMsg> _characterFollwPublisher;

		public ESceneName Name => ESceneName.Field;

		public async UniTask StartAsync()
		{
			await UniTask.Yield();

			SetUp().Forget();
		}

		public async UniTask DisposeAsync()
		{
			await UniTask.Yield();
		}

		private async UniTaskVoid SetUp()
		{
			Debug.Log($"FieldScene SetUp Start");

			_fieldSceneModel = Managers.Model.FieldScene;

			_characterFollwPublisher = Managers.MessagePipe.GetPublisher<CharacterFollow.TargetTransformMsg>();

			await CreateMapAsync();

			await UniTask.Yield();

			SpawnEnemies();

			await UniTask.Yield();

			await CreatePlayerAsync();

			await UniTask.Yield();

			await Managers.Fade.FadeInAsync();

			//TestIntervalDieEnemies();

			Debug.Log($"FieldScene SetUp Finish");
		}

		private async UniTask CreateMapAsync()
		{
			var mapPrefab = await Managers.Resource.LoadAssetAsync<GameObject>(_fieldSceneModel.MapPrefabPath, this);

			Managers.Resource.Instantiate(mapPrefab);
		}

		//CharacterSetting Component 에서 가져오자

		private async UniTask<Character> CreatePlayerAsync()
		{
			CharacterModel playerCharacterModel = Managers.Model.Character.Find(AADefine.First.CharacterModelId);

			Character playerPrefab = await Managers.Resource.LoadAssetAsync<Character>(_fieldSceneModel.PlayerPrefabPath, this);

			Character playerCharacter = Managers.Resource.Instantiate(playerPrefab);

			Managers.Object.AddPlayer(playerCharacter);

			var presenter = playerCharacter.GetOrAddComponent<PlayerCharacterPresenter>();
			presenter.Construct(Managers.Model.PlayerStat.StatSystem);

			_characterFollwPublisher.Publish(new CharacterFollow.TargetTransformMsg(playerCharacter.CachedTransform));

			return playerCharacter;
		}

		private void SpawnEnemies()
		{
			EnemyPoolSpawner enemyPoolSpawner = new GameObject().AddComponent<EnemyPoolSpawner>();
			enemyPoolSpawner.gameObject.name = nameof(EnemyPoolSpawner);

			AAPool<Character> enemy01Pool = new AAPool<Character>(_fieldSceneModel.Enemy01PrefabPath, this);
			var enemy01SpawnerSetting = new EnemyPoolSpawner.Setting() { spawnInterval = 1d, spawnPosition = new Vector3(3f, 0f, 3f) };

			enemyPoolSpawner.AddPool(enemy01Pool, enemy01SpawnerSetting);

			AAPool<Character> enemy02Pool = new AAPool<Character>(_fieldSceneModel.Enemy02PrefabPath, this);
			var enemy02SpawnerSetting = new EnemyPoolSpawner.Setting() { spawnInterval = 1d, spawnPosition = new Vector3(0f, 0f, 3f) };

			enemyPoolSpawner.AddPool(enemy02Pool, enemy02SpawnerSetting);

			AAPool<Character> enemy03Pool = new AAPool<Character>(_fieldSceneModel.Enemy03PrefabPath, this);
			var enemy03SpawnerSetting = new EnemyPoolSpawner.Setting() { spawnInterval = 1d, spawnPosition = new Vector3(-3f, 0f, 3f) };

			enemyPoolSpawner.AddPool(enemy03Pool, enemy03SpawnerSetting);
		}

		//private void TestIntervalDieEnemies()
		//{
		//	Observable.Interval(System.TimeSpan.FromSeconds(1.5d)).Subscribe(_ =>
		//	{
		//		if (Managers.Object.Enemies.Count > 0)
		//		{
		//			int rnd = UnityEngine.Random.Range(0, Managers.Object.Enemies.Count);

		//			var enemies = Managers.Object.Enemies.Values.ToList();
		//			enemies[rnd].Die();
		//		}
		//	}).AddTo(this);
		//}
	}
}