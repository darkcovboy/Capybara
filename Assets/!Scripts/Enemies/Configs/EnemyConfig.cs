using UnityEngine;

namespace Enemies.Configs
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Enemies/Enemy")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private bool _haveBreak;
        [SerializeField, Range(3, 7)] private float _timeToBreak;
        [SerializeField, Range(10, 30)] private float _timeBeforeBreak; 

        public bool HaveBreak => _haveBreak;

        public float TimeToBreak => _timeToBreak;

        public float TimeBeforeBreak => _timeBeforeBreak;
    }
}