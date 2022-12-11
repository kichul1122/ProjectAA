using UniRx;
using UnityEngine;

namespace AA
{
    [System.Serializable]
    public class CharacterClientData
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
