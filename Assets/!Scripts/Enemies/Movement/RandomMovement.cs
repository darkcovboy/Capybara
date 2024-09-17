using Enemies.Configs;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Movement
{
    public class RandomMovement : IMovement
    {
        private readonly float _range;
        private readonly NavMeshAgent _agent;
        private readonly Transform _target;
        private bool _canMove;

        public RandomMovement(NavMeshAgent agent, Transform target, RandomMovementConfig config)
        {
            _agent = agent;
            _target = target;
            _range = config.Range;
            _agent.speed = config.Speed;
        }

        public void Move()
        {
            if(!_canMove) return;
            
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {
                Vector3 point;
                if (RandomPoint(_target.position, _range, out point)) //pass in our centre point and radius of area
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                    _agent.SetDestination(point);
                }
            }
        }
        
        private bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
            { 
                //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
                //or add a for loop like in the documentation
                result = hit.position;
                return true;
            }

            result = Vector3.zero;
            return false;
        }
        
        public void StopMovement() => _canMove = false;

        public void StartMovement() => _canMove = true;

    }
}