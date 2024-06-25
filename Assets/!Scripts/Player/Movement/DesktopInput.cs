using Const;
using UnityEngine;

namespace Player.Movement
{
    public class DesktopInput : IInput
    {
        public Vector3 GetInputDirection()
        {
            float horizontalInput = SimpleInput.GetAxis(UsefulConst.HorizontalAxis);
            float verticalInput = SimpleInput.GetAxis(UsefulConst.VerticalAxis);

            if(horizontalInput == 0 && verticalInput == 0)
            {
                horizontalInput = Input.GetAxis(UsefulConst.HorizontalAxis);
                verticalInput = Input.GetAxis(UsefulConst.VerticalAxis);
            }

            Vector3 moveDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            return moveDirection;
        }
    }
}