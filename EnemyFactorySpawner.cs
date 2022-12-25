using UnityEngine;

namespace AA
{
	public class EnemyFactorySpawner : MonoBehaviour
	{
		//private CharacterFactory factory;
		private Setting setting;

		public Character[] CharacterPrefabs;

		//public void Construct(CharacterFactory factory, Setting setting)
		//{
		//	this.factory = factory;
		//	this.setting = setting;

		//	Observable.Interval(TimeSpan.FromSeconds(setting.spawnInterval)).Subscribe(_ =>
		//	{
		//		//CreateEnemyWithFactory();
		//	}).AddTo(this);

		//	//Observable.Interval(TimeSpan.FromSeconds(setting.spawnInterval)).Subscribe(_ =>
		//	//{
		//	//	CreateEnemyWithFactoryWithParams();
		//	//}).AddTo(this);
		//}

		//private void CreateEnemyWithFactory()
		//{
		//	var rnd = UnityEngine.Random.Range(0, CharacterPrefabs.Length);
		//	var character = factory.Create(CharacterPrefabs[rnd]);
		//}

		//private void CreateEnemyWithFactoryWithParams()
		//{
		//	var rnd = UnityEngine.Random.Range(0, CharacterPrefabs.Length);
		//	var character = factory.Create(CharacterPrefabs[rnd], transform.position);
		//}

		public class Setting
		{
			public double spawnInterval = 1d;
		}
	}
}
