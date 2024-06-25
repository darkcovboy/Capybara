using System;
using Player.Movement;
using UnityEngine;

namespace Player
{
    public class PlayerBody : MonoBehaviour, IWatch
    {
        public Transform Transform => gameObject.transform;
    }

    public interface IWatch
    {
        public Transform Transform { get; }
    }
}