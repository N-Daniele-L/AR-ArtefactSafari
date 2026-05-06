using System;
using UnityEngine;

namespace GameManager.Runtime
{
    public class GameCycleManager : MonoBehaviour
    {
        
        #region Unity API

        private void Start()
        {
            _gameState = GameState.STARTING;
        }

        private void Update()
        {
            switch (_gameState)
            {
                case GameState.none:
                    break;
                case GameState.STARTING:
                    StartGame();
                    break;
                case GameState.RUNNING:
                    _timer += Time.deltaTime;
                    if (_timer >= _chronoInSeconds)
                    {
                        SendEndGame();
                    }
                    break;
                case GameState.ENDED:
                    break;
                case GameState.PAUSED:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            _debugGameManager.DisplayChrono(_chronoInSeconds - _timer);
            _debugGameManager.DisplayGameCycle(_gameState.ToString());
        }

        #endregion

        #region Utils

        private void StartGame()
        {
            Debug.Log("Game initialise");
            _timer = 0f;
            _gameState = GameState.RUNNING;
        }

        private void SendEndGame()
        {
            Debug.Log("Game end");
            _gameState = GameState.ENDED;
        }

        #endregion

        #region Private

        [Header("Gameplay reference"), SerializeField] private float _chronoInSeconds;
        private float _timer;
         private enum GameState
        {
         none,
         STARTING,
         RUNNING,
         ENDED,
         PAUSED,
        }
         
        [SerializeField] private GameState _gameState;
        
        [Header("Debug References")]
        [SerializeField]private DebugGameManager _debugGameManager;

        #endregion
    }
}