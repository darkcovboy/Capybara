using UnityEngine;

namespace Player
{
    public class CharacterPoint : MonoBehaviour
    {
        public bool HaveCharacter => _currentCharacter != null;
        
        private Character _currentCharacter;

        public void SetCharacter(Character character) => _currentCharacter = character;
    }
}