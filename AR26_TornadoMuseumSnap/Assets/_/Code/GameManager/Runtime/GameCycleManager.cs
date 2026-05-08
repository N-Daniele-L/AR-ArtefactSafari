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
        
        
        #region Unity API

        private void Start()
        {
            _gameState = GameState.STARTING;
            _btnScreenShot.SetActive(false);
            Application.targetFrameRate = 60;
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
                    _timerText.text = (_chronoInSeconds - _timer).ToString(("F0"));
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
        }

        #endregion

        #region Main Methods

        public void RestartGame() => SceneManager.LoadScene(0);
        public void CloseGame() => Application.Quit();

        #endregion

        #region Utils

        private void StartGame()
        {
            bool allDesignedArtifactSpawned = _artifactManager.RunGame(_beginArtefactCount);
            _tornadoObject.SetActive(true);
            _timerText.gameObject.SetActive(true);
            _timer = 0f;
            if(allDesignedArtifactSpawned) _gameState = GameState.RUNNING;
        }

        private void SendEndGame()
        {
            _btnScreenShot.SetActive(false);
            _tornadoObject.SetActive(false);
            _timerText.gameObject.SetActive(false);
            _artifactManager.EndSpawnArtifact(true);
            _screenShotData = _scoreFromScreenshotManager.EndScoreData();
            _endPanels.SetActive(true);
            DisplayEndScore();
            _gameState = GameState.ENDED;
        }

        private void DisplayEndScore()
        {
            int totalScore = 0;
            for (int i = 0; i < _screenShotData.Count; i++)
            {
                _artefactPanel[i].SetActive(true);
                _rawImages[i].texture = _screenShotData[i].m_shotTexture;
                if(_screenShotData[i].m_objectHit != null) _titleImages[i].text = _screenShotData[i].m_objectHit.name;
                else _titleImages[i].text = "Nothing";
                _scoreImages[i].text = _screenShotData[i].m_screenShotScore.ToString();
                totalScore += _screenShotData[i].m_screenShotScore;
            }
            _totalScoreText.text =  totalScore.ToString();
        }

        #endregion

        #region Private

        private float _timer;
         private enum GameState
        {
         none,
         STARTING,
         RUNNING,
         ENDED,
         PAUSED,
        }
         
        [Header("Dev References")] 
        [SerializeField] private GameState _gameState;
        [SerializeField] private ScoreFromScreenshotManager  _scoreFromScreenshotManager;
        [SerializeField] private ArtifactManager _artifactManager;
        [SerializeField] private GameObject _tornadoObject;
        [Header("Design Reference")]
        [SerializeField] private int _beginArtefactCount;
        [SerializeField] private float _chronoInSeconds;
        [Header("UI References")]
        [SerializeField] private TMP_Text _totalScoreText;

        [SerializeField] private List<GameObject> _artefactPanel;
        [SerializeField] private RawImage[] _rawImages;
        [SerializeField] private TMP_Text[] _titleImages;
        [SerializeField] private TMP_Text[] _scoreImages;
        [SerializeField] private GameObject _endPanels;
        [SerializeField] private GameObject _btnScreenShot;
        [SerializeField] private TMP_Text _timerText;

        private List<ScreenShotData> _screenShotData;
        
        #endregion
    }
}