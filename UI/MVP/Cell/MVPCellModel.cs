using Sirenix.OdinInspector;
using System;
using System.Collections;
using UniRx;
using UnityEngine;

namespace AA
{
    [System.Serializable]
    public class MVPCellModel : IDisposable
    {
        public long Id;

        [ShowInInspector]
        [DisableInPlayMode]
        public ReactiveProperty<int> Count;

        public void Dispose()
        {
            Count.Dispose();
        }

        [Button]
        private void CountUp()
        {
            Count.Value++;
        }
    }
}