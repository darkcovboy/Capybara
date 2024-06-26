using System;
using UnityEngine;

namespace Player.Animation
{
    public class CharacterAddAnimation : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _prefab;
        [SerializeField] private CharactersGroupHolder _charactersGroupHolder;
        
        private void OnEnable()
        {
            _charactersGroupHolder.OnCharacterAdded += PlayAnimation;
        }

        private void OnDisable()
        {
            _charactersGroupHolder.OnCharacterAdded -= PlayAnimation;
        }

        private void PlayAnimation(Vector3 position)
        {
            var particle = Instantiate(_prefab, position, Quaternion.identity);
            var mainModule = particle.main;
            Destroy(particle.gameObject, mainModule.duration + mainModule.startLifetime.constantMax);
        }
    }
}