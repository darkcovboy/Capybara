using UnityEngine;

namespace Player.PlayerStaticData
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Character/Data", order = 0)]
    public class CharacterData : ScriptableObject
    {
        public float Speed = 6f;
    }
}