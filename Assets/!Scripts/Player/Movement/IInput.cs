using UnityEngine;

namespace Player.Movement
{
    public interface IInput
    {
        Vector3 GetInputDirection();
    }
}