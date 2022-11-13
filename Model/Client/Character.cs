using UniRx;
using UnityEngine;

namespace AA.Client
{
    [System.Serializable]
    public class Character
    {
        public ReactiveProperty<Vector3> Position = new ReactiveProperty<Vector3>();

        public double Attack;
        public double MoveSpeed;
        public double AttackSpeed;
        public double SearchDistance;
        public double AttackDistance;
        public double MaxHp;

        public double CurrentHp;

        public class Status
        { }

        public class Attributes
        { }
    }
}
