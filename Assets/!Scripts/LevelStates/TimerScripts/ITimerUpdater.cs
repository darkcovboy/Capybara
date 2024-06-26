using System;

namespace LevelStates.TimerScripts
{
    public interface ITimerUpdater
    {
        event Action<int, int> OnTimeUpdated;
    }
}