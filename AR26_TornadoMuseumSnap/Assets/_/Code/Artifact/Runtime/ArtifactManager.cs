using System;
using PoolSystem_RunTime;
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

        #endregion
        
        #region Utils

        private void SpawnArtifact()
        {
            _time += Time.deltaTime;
            if (_time <= _spawnTimer) return;
            GameObject artifact = m_poolArtifact.GetArtefact();
            ArtifactBehaviour behaviour = artifact.GetComponent<ArtifactBehaviour>();
            behaviour.SetArtifactManager(this);
            artifact.SetActive(true);
            _time = 0f;
        }
        
        #endregion

        
        #region Privates

        private float _time = 0;
        [SerializeField] private float _spawnTimer = 1f;

        #endregion
    }
}