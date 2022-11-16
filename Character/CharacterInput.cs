
using UnityEngine;

namespace AA
{
    public class CharacterInput : CMF.CharacterInput
    {
        public Vector3 Direction;

        public override float GetHorizontalMovementInput()
        {
            return Direction.x;
        }

        public override float GetVerticalMovementInput()
        {
            return Direction.z;
        }

        public override bool IsJumpKeyPressed()
        {
            return false;
        }
    }
}
