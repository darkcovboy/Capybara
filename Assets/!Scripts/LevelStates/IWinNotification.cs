using System;

namespace LevelStates
{
    public interface IWinNotification
    {
        public event Action OnGameWin;
    }
}