using CMF;

namespace Player
{
    public class PlayerInputMovement : CharacterInput
    {
        private float horizontalMovement;
        private float verticalMovement;
        private bool isJumpPressed;
        
        public override float GetHorizontalMovementInput()
        {
            return horizontalMovement;
        }

        public override float GetVerticalMovementInput()
        {
            return verticalMovement;
        }

        public override bool IsJumpKeyPressed()
        {
            return isJumpPressed;
        }

        public void PlayerMove(float horizontal, float vertical, bool jump)
        {
            horizontalMovement = horizontal;
            verticalMovement = 0;
            isJumpPressed = jump;
        }
        
        
    }
}