using LevelStates.TimerScripts;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.InfoShower
{
    public class TimerShower : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        private ITimerUpdater _timer;

        [Inject]
        public void Constructor(ITimerUpdater timerUpdater)
        {
            _timer = timerUpdater;
            _timer.OnTimeUpdated += UpdateTime;
        }

        private void UpdateTime(int minutes, int seconds) => _text.text = $"{minutes}:{seconds}";
    }
}