using System;
using Agava.YandexGames;
using UnityEngine;
using YG;

namespace Ads
{
    public class ADManager
    {
        private float _timeScale;
        private bool _isPauseAudioListener;
        private float _volume;

        public void ShowAd()
        {
            InterstitialAd.Show(PauseGameplay, ResumeGameplay);
        }

        public void ShowRewardedAd(Action onReward)
        {
            VideoAd.Show(PauseGameplay, onReward, ResumeGameplay);
        }

        private void PauseGameplay()
        {
            _timeScale = Time.timeScale;
            _isPauseAudioListener = AudioListener.pause;
            _volume = AudioListener.volume;

            Time.timeScale = 0f;
            AudioListener.pause = true;
            AudioListener.volume = 0f;
        }

        private void ResumeGameplay(bool obj)
        {
            Time.timeScale = _timeScale;
            AudioListener.pause = _isPauseAudioListener;
            AudioListener.volume = _volume;
        }
        
        private void ResumeGameplay()
        {
            Time.timeScale = _timeScale;
            AudioListener.pause = _isPauseAudioListener;
            AudioListener.volume = _volume;
        }
    }
}