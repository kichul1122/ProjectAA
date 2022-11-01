using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace AA
{
    public class CharacterInput : MonoBehaviour
    {
        public IReadOnlyReactiveProperty<bool> JumpButton => _jumpButton;

        private readonly ReactiveProperty<bool> _jumpButton = new ReactiveProperty<bool>(false);
        
        private void Start()
        {
            _jumpButton.AddTo(this);
        }

        private void Update()
        {
            _jumpButton.Value = Input.GetButton("Jump");
        }
    }
}
