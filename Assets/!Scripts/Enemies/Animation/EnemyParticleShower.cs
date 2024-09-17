using Extension;
using UnityEngine;

namespace Enemies.Animation
{
    public class EnemyParticleShower : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _sleepSystem;
        [SerializeField] private ParticleSystem _happyParticle;

        public void SwitchHappyParticle(bool isEnabled)
        {
            _happyParticle.gameObject.SetActive(isEnabled);
        }

        public void SwitchSleepParticle(bool isEnabled)
        {
            _sleepSystem.gameObject.SetActive(isEnabled);
        }
    }
}