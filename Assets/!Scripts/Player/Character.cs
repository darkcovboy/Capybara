using Player.Movement;
using Player.PlayerStaticData;
using UnityEngine;

namespace Player
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private MovementHandler _movementHandler;
        
        private CharacterData _characterData;
        private IInput _input;

        public void Init(CharacterData characterData, IInput input)
        {
            _characterData = characterData;
            _input = input;

            Setup();
        }

        private void Setup()
        {
            _movementHandler.Init(_characterData.Speed, _input);
        }
    }
}