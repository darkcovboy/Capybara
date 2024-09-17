using Enemies.Configs;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Movement
{
    public class PointToPointMovement : IMovement
    {
        private readonly NavMeshAgent _agent;
        private readonly Transform[] _waypoints;

        private bool _canMove;
        private int _currentWaypointIndex = 0;

        public PointToPointMovement(NavMeshAgent agent, Transform[] points, PointToPointMovementConfig config)
        {
            _waypoints = points;
            _agent = agent;
            _agent.speed = config.Speed;
        }

        public void Move()
        {
            if (_waypoints.Length == 0) return;
            
            if(!_canMove) return;

            if (_agent.remainingDistance < _agent.stoppingDistance)
            {
                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
                _agent.SetDestination(_waypoints[_currentWaypointIndex].position);
            }
        }

        public void StopMovement() => _canMove = false;

        public void StartMovement() => _canMove = true;
    }
}