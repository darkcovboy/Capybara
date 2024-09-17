using UnityEngine;

namespace Enemies.Configs
{
    [CreateAssetMenu(fileName = "RandomMovementConfig", menuName = "Enemies/Movement/Random")]
    public class RandomMovementConfig : ScriptableObject
    {
        [SerializeField] private float _range = 10;
        [SerializeField] private float _speed = 1;

        public float Range => _range;
        public float Speed => _speed;
    }
}