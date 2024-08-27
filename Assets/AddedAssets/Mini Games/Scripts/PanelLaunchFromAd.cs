using System;
using UnityEngine;

namespace MiniGames
{
    public class PanelLaunchFromAd : MonoBehaviour
    {
        [SerializeField] private Gateway _gateway;
        private event Action _action;


        public void StartAD() {
            Action<bool> callBack = (success) => {
                if (success)
                    _action.Invoke();
            };

            _gateway.StartRewardAD(callBack);

            Hide();
        }

        public void Show(Action callBack) {
            _action = callBack;

            gameObject.SetActive(true);
        }

        public void Hide() {
            gameObject.SetActive(false);
        }
    }
}
