using UniRx;
using UnityEngine;

namespace AA
{
    public class EnemyPoolSpawner : MonoBehaviour
    {
        //private CharacterPool pool;
        //private Setting setting;

        public void AddPool(AAPool<Character> pool, Setting setting)
        {
            StartSpawn(pool, setting);
        }

        private void StartSpawn(AAPool<Character> pool, Setting setting)
        {
            //Observable.Interval(TimeSpan.FromSeconds(setting.spawnInterval)).Subscribe(_ =>
            //{
            Character newCharacter = pool.Rent();
            newCharacter.Observable.OnRemoveObservable().Subscribe(character => pool.Return(character)).AddTo(this);

            newCharacter.Construct().SetParent(transform).Teleport(setting.spawnPosition);

            Managers.Object.AddEnemy(newCharacter);

            //}).AddTo(this);
        }

        public class Setting
        {
            public double spawnInterval = 1d;
            public Vector3 spawnPosition = Vector3.zero;
        }
    }
}
