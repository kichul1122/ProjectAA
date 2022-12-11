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
			_fieldSceneModel = Managers.Model.FieldScene;

			_characterFollwPublisher = Managers.MessagePipe.GetPublisher<CharacterFollow.TargetTransformMsg>();

			await CreateMapAsync();

			await SpawnEnemiesAsync();

			await CreatePlayerAsync();

			await Managers.Fade.FadeInAsync();

			//TestIntervalDieEnemies();
		}

		private async UniTask CreateMapAsync()
		{
			await Managers.Resource.InstantiateAsync(_fieldSceneModel.MapPrefabPath, this);
		}

		//CharacterSetting Component 에서 가져오자

		private async UniTask<Character> CreatePlayerAsync()
		{
			CharacterModel playerCharacterModel = Managers.Model.Character.Find(Managers.Model.DefaultPlayerId);

			Character playerCharacter = await CharacterFactory.Default.CreateAsync(_fieldSceneModel.PlayerPrefabPath, playerCharacterModel, this);
			Managers.Object.AddPlayer(playerCharacter);

			_characterFollwPublisher.Publish(new CharacterFollow.TargetTransformMsg(playerCharacter.CachedTransform));

			return playerCharacter;
		}

		private async UniTask SpawnEnemiesAsync()
		{
			EnemyPoolSpawner enemyPoolSpawner = new GameObject().AddComponent<EnemyPoolSpawner>();
			enemyPoolSpawner.gameObject.name = nameof(EnemyPoolSpawner);

			var enemyPrefab = await Managers.Resource.LoadPrefabAsync(_fieldSceneModel.EnemyPrefabPath, this);
			CharacterPool enemyPool = new CharacterPool(enemyPrefab);

			var enemySpawnerSetting = new EnemyPoolSpawner.Setting() { spawnInterval = 1d };
			enemyPoolSpawner.Construct(enemyPool, enemySpawnerSetting, Managers.Object);
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