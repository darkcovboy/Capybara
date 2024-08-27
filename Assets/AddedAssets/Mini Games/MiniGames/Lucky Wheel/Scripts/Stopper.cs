using UnityEngine;

namespace FortuneWheel
{
    public class Stopper : MonoBehaviour
    {
        private Animator anim;


        private void Start() {
            anim = transform.GetComponent<Animator>();
        }

        public void Play() {
            anim.SetTrigger("isPlay");
        }

        private void OnEnable() {
            transform.rotation = Quaternion.identity;
        }
    }
}