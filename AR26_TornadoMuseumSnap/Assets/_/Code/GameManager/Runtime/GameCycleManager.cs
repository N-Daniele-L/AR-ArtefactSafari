using System;
using System.Collections.Generic;
using Artifact.Runtime;
using Data.Runtime;
using ScoreManager.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        #region Main Methods

        public void RestartGame() => SceneManager.LoadScene(0);
        public void CloseGame() => Application.Quit();

        #endregion

        #region Utils

        private void StartGame()
        {
            Debug.Log("Game initialise");
            bool allDesignedArtifactSpawned = m_artifactManager.RunGame(_beginArtefactCount);
            _timer = 0f;
            if(allDesignedArtifactSpawned) _gameState = GameState.RUNNING;
        }

        private void SendEndGame()
        {
            Debug.Log("Game end");
            _btnScreenShot.SetActive(false);
            m_artifactManager.EndSpawnArtifact(true);
            _screenShotData = _scoreFromScreenshotManager.EndScoreData();
            _endPanels.SetActive(true);
            DisplayEndScore();
            _gameState = GameState.ENDED;
        }

        private void DisplayEndScore()
        {
            for (int i = 0; i < _screenShotData.Count; i++)
            {
                _rawImages[i].texture = _screenShotData[i].m_shotTexture;
                if(_screenShotData[i].m_objectHit != null) _titleImages[i].text = _screenShotData[i].m_objectHit.name;
                else _titleImages[i].text = "no artefact";
                _scoreImages[i].text = _screenShotData[i].m_screenShotScore.ToString();
            }
        }

        #endregion

        #region Private

        [Header("Gameplay reference"), SerializeField] private float _chronoInSeconds;
        private float _timer = 0;
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
        [SerializeField] private ScoreFromScreenshotManager  _scoreFromScreenshotManager;
        private List<ScreenShotData> _screenShotData;
        [SerializeField] private RawImage[] _rawImages;
        [SerializeField] private TMP_Text[] _titleImages;
        [SerializeField] private TMP_Text[] _scoreImages;
        [SerializeField] private GameObject _endPanels;

        #endregion
    }
}