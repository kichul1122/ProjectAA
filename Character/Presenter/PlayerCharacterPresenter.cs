using CMF;
using UniRx;
using UnityEngine;

namespace AA
{
    public class PlayerCharacterPresenter : MonoBehaviour
    {
        private StatSystem _statSystem;

        public void Construct(StatSystem statSystem)
        {
            _statSystem = statSystem;

            FormulaSystem formulaSystem = new(statSystem);

            var move = GetComponent<SimpleWalkerController>();
            if (move)
            {
                formulaSystem.AsObservable(EStatAttribute.MoveSpeed).Subscribe(value =>
                {
                    move.movementSpeed = (float)value;
                });
            }
        }
    }
}