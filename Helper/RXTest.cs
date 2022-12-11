using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace AA
{
    public class RXTest : MonoBehaviour
    {
        CompositeDisposable _disposables;


        [Button]
        public void Allocate()
        {
            _disposables = new CompositeDisposable();

        }

        private int constructorCount;

        [Button]
        public void Constructor()
        {
            _disposables?.Clear();

            ++constructorCount;


            Observable.Interval(System.TimeSpan.FromSeconds(1d)).Subscribe(_ => Debug.Log($"{constructorCount}")).AddTo(_disposables);
        }

        [Button]
        public void OnDestroy()
        {
            _disposables?.Dispose();
            _disposables = null;

        }

    }
}
