using Enemies.Catcher;
using UnityEngine;

namespace Enemies.Fabric
{
    [CreateAssetMenu(fileName = "EnemyFabric", menuName = "Enemies/Fabric")]
    public class EnemyFabric : ScriptableObject
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Cage _cagePrefab;

        public Enemy Get(Vector3 position)
        {
            Enemy enemy = Instantiate(_enemyPrefab, position, Quaternion.identity);
            return enemy;
        }
        
        public Cage GetCage(Vector3 at)
        {
            Cage cage = Instantiate(_cagePrefab, at, Quaternion.identity);
            return cage;
        }
    }
}