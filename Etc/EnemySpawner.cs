using UnityEngine;

namespace AA
{
	public class EnemySpawner : MonoBehaviour
	{
		//CharacterPool pool;
		//CharacterFactory factory;
		Setting setting;

		public Character[] CharacterPrefabs;

		//public void Construct(CharacterPool pool, Setting setting)
		//{
		//	this.pool = pool;
		//	this.setting = setting;

		//	//Observable.Interval(TimeSpan.FromSeconds(setting.spawnInterval)).Subscribe(_ =>
		//	//{
		//	//	CreateEnemyWithFactory();
		//	//}).AddTo(this);

		//	//Observable.Interval(TimeSpan.FromSeconds(setting.spawnInterval)).Subscribe(_ =>
		//	//{
		//	//	CreateEnemyWithFactoryWithParams();
		//	//}).AddTo(this);

		//	Observable.Interval(TimeSpan.FromSeconds(setting.spawnInterval)).Subscribe(_ =>
		//	{
		//		pool.Rent();
		//	}).AddTo(this);
		//}

		private float _spawnTimer;

		public class Setting
		{
			public double spawnInterval = 1d;
		}
	}
}
