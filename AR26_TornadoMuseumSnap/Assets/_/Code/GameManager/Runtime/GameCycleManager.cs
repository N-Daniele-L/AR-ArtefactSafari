using System;
using Artifact.Runtime;
using UnityEngine;

namespace GameManager.Runtime
{
    public class GameCycleManager : MonoBehaviour
    {
        #region Publics
        
            public ArtifactManager m_artifactManager;
            public GameObject _btnScreenShot;

        
        #endregion
        
        
        #region Unity API

        private void Start()
        {
            _gameState = GameState.STARTING;
            _btnScreenShot.SetActive(false);
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
                    _btnScreenShot.SetActive(true);
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
            bool _allDesignedArtifactSpawned = m_artifactManager.RunGame(_beginArtefactCount);
            _timer = 0f;
            if(_allDesignedArtifactSpawned) _gameState = GameState.RUNNING;
        }

        private void SendEndGame()
        {
            Debug.Log("Game end");
            _btnScreenShot.SetActive(false);
            m_artifactManager.EndSpawnArtifact(true);
            //block input
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
        [SerializeField] private int _beginArtefactCount;

        #endregion
    }
}