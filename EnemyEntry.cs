using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace AA
{
    [System.Serializable]
    public class EnemyEntry : MonoBehaviour
    {
        public List<Character> enemies = new List<Character>();

        public void Add(Character newCharacter)
        {
            enemies.Add(newCharacter);

            newCharacter.Observable.OnRemoveObservable().Subscribe(_ => enemies.Remove(_)).AddTo(this);
        }
    }
}
