using UnityEngine;

namespace Enemies.Animation
{
    public class EnemyAnimation : MonoBehaviour
    {
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Movement = Animator.StringToHash("Movement");
        private static readonly int Break = Animator.StringToHash("Break");
        private static readonly int CatchBreak = Animator.StringToHash("CatchBreak");
        
        [SerializeField] private Animator _animator;
        
        public void PlayMovement()
        {
            _animator.SetTrigger(Movement);
        }

        public void PlayAttack()
        {
            _animator.SetTrigger(Attack);
        }

        public void PlayBreakAfterAttack()
        {
            _animator.SetTrigger(Break);
        }

        public void PlayBreaking()
        {
            _animator.SetTrigger(CatchBreak);
        }
    }
}