using UnityEngine;

namespace DropGame
{
    public class TouchAudio : MonoBehaviour
    {
        [SerializeField] private AudioSource _audio;


        private void OnCollisionEnter2D(Collision2D collision) {
            _audio.Play();
        }
    }
}
