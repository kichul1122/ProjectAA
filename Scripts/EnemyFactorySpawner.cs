using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace AA
{
	public class EnemyFactorySpawner : MonoBehaviour
	{
		private Character.Factory factory;
		private Setting setting;

		public Character[] CharacterPrefabs;

		public void Constructor(Character.Factory factory, Setting setting)
		{
			this.factory = factory;
			this.setting = setting;

			Observable.Interval(TimeSpan.FromSeconds(setting.spawnInterval)).Subscribe(_ =>
			{
				CreateEnemyWithFactory();
			}).AddTo(this);

			//Observable.Interval(TimeSpan.FromSeconds(setting.spawnInterval)).Subscribe(_ =>
			//{
			//	CreateEnemyWithFactoryWithParams();
			//}).AddTo(this);
		}

		private void CreateEnemyWithFactory()
		{
			var rnd = UnityEngine.Random.Range(0, CharacterPrefabs.Length);
			var character = factory.Create(CharacterPrefabs[rnd]);
		}

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
