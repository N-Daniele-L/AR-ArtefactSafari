using System;
using PoolSystem.RunTime;
using UnityEngine;
using UnityEngine.Splines;

namespace Artifact.Runtime
{
    public class ArtifactManager : MonoBehaviour
    {
        #region Publics

        public PrefabPool m_poolArtifact;
        public SplineContainer[] m_splineContainers;

        #endregion
        
        
        #region Unity API
        

        private void Update()
        {
            if (_countArtifactAlive >= _maxArtifactAlive) return;
            SpawnArtifact();
        }

        #endregion

        
        #region Main Methods

        public SplineContainer GetRandomSplineContainer()
        {
            System.Random randomSplineContainer = new System.Random();
            int randomIndex = randomSplineContainer.Next(m_splineContainers.Length);
            SplineContainer splineContainer = m_splineContainers[randomIndex];
            return splineContainer;
        }
        
        public void ArtifactDespawned(GameObject artifact)
        {
            _countArtifactAlive--;
        }

        public bool RunGame(int beginGameAtArtifactCount)
        {
            return !(_countArtifactAlive < beginGameAtArtifactCount);
        }

        public void EndSpawnArtifact(bool isGameOver)
        {
            _gameIsOver =  isGameOver;
        }

        #endregion
        
        #region Utils

        private void SpawnArtifact()
        {
            _time += Time.deltaTime;
            if (_time <= _spawnTimerInSecond) return;
            GameObject artifact = m_poolArtifact.GetArtefact();
            ArtifactBehaviour behaviour = artifact.GetComponent<ArtifactBehaviour>();
            behaviour.SetArtifactManager(this);
            artifact.SetActive(true);
            _time = 0f;
            _countArtifactAlive++;
        }

        
        
        #endregion

        
        #region Privates

        private float _time = 0;
        [SerializeField] private float _spawnTimerInSecond = 1f;
        private float _countArtifactAlive;
        private bool _gameIsOver = false;
        [SerializeField] private float _maxArtifactAlive = 10;


        #endregion
    }
}