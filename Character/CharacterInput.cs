using UniRx;
using UnityEngine;

namespace AA
{
    public class CharacterInput : CMF.CharacterInput
    {
        public IReadOnlyReactiveProperty<bool> JumpButton => _jumpButton;

        private readonly ReactiveProperty<bool> _jumpButton = new ReactiveProperty<bool>(false);

        private InputManager _input;

        private void Start()
        {
            _jumpButton.AddTo(this);

            _input = Managers.Input;

        }

        private void Update()
        {
            _jumpButton.Value = Input.GetButton("Jump");
        }

        public override float GetHorizontalMovementInput()
        {
            return _input.Horizontal;
        }

        public override float GetVerticalMovementInput()
        {
            return _input.Vertical;
        }

        public override bool IsJumpKeyPressed()
        {
            return _jumpButton.Value;
        }
    }
}
