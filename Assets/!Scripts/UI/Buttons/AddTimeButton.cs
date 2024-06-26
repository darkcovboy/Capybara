using System;
using DefaultNamespace;
using LevelStates.TimerScripts;
using UnityEngine;
using Zenject;

namespace UI.Buttons
{
    public class AddTimeButton : RewardButton
    {
        [SerializeField, Range(5, 15)] private int _timeToAdd;
        
        private ITimerAdd _timerAdd;

        [Inject]
        public void Constructor(ITimerAdd timerAdd)
        {
            _timerAdd = timerAdd;
        }

        protected override void HandleReward()
        {
            YandexMain.Instance.ADManager.ShowRewardedAd(()=>_timerAdd.AddTime(_timeToAdd));
        }
    }
}