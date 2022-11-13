using Cysharp.Threading.Tasks;
using System.Linq;
using UniRx;
using UnityEngine;

namespace AA
{
	public class FieldScene : MonoBehaviour, IScene
	{
		private FieldSceneModel _fieldSceneModel;

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

		private Subject<Player> OnStart = new Subject<Player>();

		private async UniTaskVoid SetUp()
		{
			_fieldSceneModel = Managers.Model.FieldScene;

			await CreateMapAsync();

			await SpawnEnemiesAsync();

			await CreatePlayerAsync();

			await Managers.Fade.FadeInAsync();

			StartField();

			TestIntervalDieEnemies();
		}

		private async UniTask CreateMapAsync()
		{
			var mapPrefab = await Managers.Resource.LoadPrefabAsync(_fieldSceneModel.MapPrefabPath, this);
			Managers.Resource.Instantiate(mapPrefab);
		}

		private async UniTask<Character> CreatePlayerAsync()
		{
			CharacterModel playerCharacterModel = Managers.Model.Character.Find(Managers.Model.DefaultPlayerId);
			Character.Factory playerFactory = new Character.Factory();
			Character playerCharacter = await playerFactory.CreateAsync(_fieldSceneModel.PlayerPrefabPath, playerCharacterModel, this);
			Managers.Object.AddPlayer(playerCharacter);

			return playerCharacter;
		}

		private async UniTask SpawnEnemiesAsync()
		{
			EnemyPoolSpawner enemyPoolSpawner = new GameObject().AddComponent<EnemyPoolSpawner>();
			enemyPoolSpawner.gameObject.name = nameof(EnemyPoolSpawner);

			var enemyPrefab = await Managers.Resource.LoadPrefabAsync(_fieldSceneModel.EnemyPrefabPath, this);
			Character.Pool enemyPool = new Character.Pool(enemyPrefab);

			var enemySpawnerSetting = new EnemyPoolSpawner.Setting() { spawnInterval = 1d };
			enemyPoolSpawner.Construct(enemyPool, enemySpawnerSetting, Managers.Object);
		}

		private void TestIntervalDieEnemies()
		{
			Observable.Interval(System.TimeSpan.FromSeconds(1.5d)).Subscribe(_ =>
			{
				if (Managers.Object.Enemies.Count > 0)
				{
					int rnd = UnityEngine.Random.Range(0, Managers.Object.Enemies.Count);

					var enemies = Managers.Object.Enemies.Values.ToList();
					enemies[rnd].Die();
				}
			}).AddTo(this);
		}

		private void StartField()
		{
			// OnStart.Subscribe(currentPlayer => currentPlayer.DoStart()).AddTo(this);
		}
	}
}