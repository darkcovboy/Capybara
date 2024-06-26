using UnityEngine;

namespace LevelStates.Data
{
    [CreateAssetMenu(fileName = "Level", menuName = "Settings")]
    public class LevelData : ScriptableObject
    {
        [SerializeField, Range(30, 240)] private int _time;

        public int Time => _time;
    }
}