using System;
using Player;
using Player.ItemsAction;
using UnityEngine;

namespace Enemies.Catcher
{
    public class CatcherTriggerObserver : MonoBehaviour, ITriggerObserver<Character>
    {
        public event Action<Character> Enter;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Character character))
            {
                Debug.Log($"Character {other.gameObject.name} enter in catcher");
                Enter?.Invoke(character);
            }
        }

        public void Switch(bool isEnabled)
        {
            gameObject.SetActive(isEnabled);
        }
    }
}