using System;
using System.Collections;
using UnityEngine;

namespace LevelStates.TimerScripts
{
    public class Timer : ILoseNotification, ITimerUpdater, ITimerAdd
    {
        public event Action OnGameLose;
        public event Action<int, int> OnTimeUpdated;
        
        private readonly ICoroutineRunner _coroutineRunner;
        private int _time;


        public Timer(ICoroutineRunner coroutineRunner, int time)
        {
            _coroutineRunner = coroutineRunner;
            _time = time;
        }

        public void Start()
        {
            _coroutineRunner.StartCoroutine(TimerCoroutine());
        }

        public void Stop()
        {
            _coroutineRunner.StopCoroutine(TimerCoroutine());
        }

        public void AddTime(int seconds)
        {
            _time += seconds;
        }

        private IEnumerator TimerCoroutine()
        {
            int remainingTime = _time;

            while (remainingTime > 0)
            {
                int minutes = remainingTime / 60;
                int seconds = remainingTime % 60;

                OnTimeUpdated?.Invoke(minutes, seconds);

                yield return new WaitForSeconds(1f);

                remainingTime--;
            }

            OnGameLose?.Invoke();
        }
    }
}