using LevelStates.TimerScripts;
using Player;
using UnityEngine;
using Zenject;

namespace LevelStates
{
    public class GameState : MonoBehaviour
    {
        private CharactersGroupHolder _player;
        private Timer _timer;

        [Inject]
        public void Constructor(CharactersGroupHolder groupHolder, Timer timer)
        {
            _player = groupHolder;
            _timer = timer;
            Subscriptions();

            void Subscriptions()
            {
                _player.OnGameLose += LoseGame;
                _timer.OnGameLose += LoseGame;
            }
        }

        private void Start()
        {
            _timer.Start();
        }

        private void LoseGame()
        {
            
        }
    }
}