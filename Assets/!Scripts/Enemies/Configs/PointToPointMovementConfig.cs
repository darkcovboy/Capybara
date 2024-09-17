using UnityEngine;

namespace Enemies.Configs
{
    [CreateAssetMenu(fileName = "PointToPointMovementConfig", menuName = "Enemies/Movement/PointToPoint")]
    public class PointToPointMovementConfig : ScriptableObject
    {
        [SerializeField] private float _speed = 2;

        public float Speed => _speed;
    }
}