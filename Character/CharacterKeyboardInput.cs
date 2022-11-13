namespace AA
{
	public class CharacterKeyboardInput : CMF.CharacterInput
	{
		private InputManager _input;

		private void Start()
		{
			_input = Managers.Input;
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
			return _input.Jump;
		}
	}
}
