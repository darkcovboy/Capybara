using System;
using Agava.WebUtility;
using UnityEngine;

namespace DefaultNamespace
{
    public class BackgroundFocus : IDisposable
    {
        private float _timeScale;
        private bool _isPauseAudioListener;
        private float _volume;
        
        public BackgroundFocus()
        {
            Application.focusChanged += OnBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent += OnBackgroundChangeWeb;
        }

        public void Dispose()
        {
            Application.focusChanged -= OnBackgroundChangeApp;
            WebApplication.InBackgroundChangeEvent -= OnBackgroundChangeWeb;
        }

        private void OnBackgroundChangeWeb(bool inApp)
        {
            if (inApp)
                ResumeGameplay();
            else
                PauseGameplay();
        }

        private void OnBackgroundChangeApp(bool inBackground)
        {
            if (inBackground)
                ResumeGameplay();
            else
                PauseGameplay();
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