using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Movement
{
    public interface IMovement
    {
        public void Move();

        public void StopMovement();
        public void StartMovement();
    }
}