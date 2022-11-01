using System;
using UniRx;
using UnityEngine;

namespace AA
{
    public class EnemyPoolSpawner : MonoBehaviour
    {
        private Character.Pool pool;
        private Setting setting;
        private ObjectManager objectManager;

        public void Construct(Character.Pool pool, Setting setting, ObjectManager objectManager)
        {
            this.pool = pool;
            this.setting = setting;
            this.objectManager = objectManager;

            //Interval Spawn Enemy
            Observable.Interval(TimeSpan.FromSeconds(setting.spawnInterval)).Subscribe(_ =>
            {
                Character newCharacter = pool.Spawn();
                newCharacter.Construct().SetParent(transform);
                objectManager.AddEnemy(newCharacter);

            }).AddTo(this);
        }

        public class Setting
        {
            public double spawnInterval = 1d;
        }
    }
}
