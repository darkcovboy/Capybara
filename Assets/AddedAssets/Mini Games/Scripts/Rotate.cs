using UnityEngine;

namespace MiniGames
{
    public class Rotate : MonoBehaviour
    {
        public Vector3 speed;


        void Update() {
            transform.Rotate(speed * Time.deltaTime);
        }
    }
}
