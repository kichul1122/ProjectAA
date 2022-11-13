using System.Numerics;

namespace AA
{
    public class CharacterInput : CMF.CharacterInput
    {
        public Vector3 Direction;

        public override float GetHorizontalMovementInput()
        {
            return Direction.X;
        }

        public override float GetVerticalMovementInput()
        {
            return Direction.Z;
        }

        public override bool IsJumpKeyPressed()
        {
            return false;
        }
    }
}
