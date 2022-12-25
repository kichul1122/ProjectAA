using System;
using UniRx;
using UnityEngine;

namespace AA
{
	public class EnemySpawner : MonoBehaviour
	{
		CharacterPool pool;
		CharacterFactory factory;
		Setting setting;

		public Character[] CharacterPrefabs;

		public void Construct(CharacterPool pool, Setting setting)
		{
			this.pool = pool;
			this.setting = setting;

			//Observable.Interval(TimeSpan.FromSeconds(setting.spawnInterval)).Subscribe(_ =>
			//{
			//	CreateEnemyWithFactory();
			//}).AddTo(this);

			//Observable.Interval(TimeSpan.FromSeconds(setting.spawnInterval)).Subscribe(_ =>
			//{
			//	CreateEnemyWithFactoryWithParams();
			//}).AddTo(this);

			Observable.Interval(TimeSpan.FromSeconds(setting.spawnInterval)).Subscribe(_ =>
			{
				pool.Spawn();
			}).AddTo(this);
		}

		private float _spawnTimer;

		private void CreateEnemyWithFactory()
		{
			var rnd = UnityEngine.Random.Range(0, CharacterPrefabs.Length);
			var character = factory.Create(CharacterPrefabs[rnd]);
		}

		private void CreateEnemyWithFactoryWithParams()
		{
			var rnd = UnityEngine.Random.Range(0, CharacterPrefabs.Length);
			var character = factory.Create(CharacterPrefabs[rnd], transform.position);
		}

		public class Setting
		{
			public double spawnInterval = 1d;
		}
	}
}
