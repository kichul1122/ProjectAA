using UniRx;
using UnityEngine;

namespace AA
{
    public class EnemyPoolSpawner : MonoBehaviour
    {
        private CharacterPool pool;
        private Setting setting;

        public void Construct(CharacterPool pool, Setting setting)
        {
            this.pool = pool;
            this.setting = setting;

            StartSpawn();
        }

        private void StartSpawn()
        {
            //Observable.Interval(TimeSpan.FromSeconds(setting.spawnInterval)).Subscribe(_ =>
            //{
            Character newCharacter = pool.Spawn();
            newCharacter.Observable.OnRemoveAsObservable().Take(1).Subscribe(character => pool.Despawn(character)).AddTo(this);

            newCharacter.Construct().SetParent(transform);
            Managers.Object.AddEnemy(newCharacter);

            //}).AddTo(this);
        }

        public class Setting
        {
            public double spawnInterval = 1d;
        }
    }
}
