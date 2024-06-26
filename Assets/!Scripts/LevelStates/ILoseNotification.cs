using System;

namespace LevelStates
{
    public interface ILoseNotification
    {
        public event Action OnGameLose;
    }
}