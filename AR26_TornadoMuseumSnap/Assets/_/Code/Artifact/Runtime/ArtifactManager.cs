using UnityEngine;
using UnityEngine.Splines;

namespace Artifact.Runtime
{
    public class ArtifactManager : MonoBehaviour
    {
        #region Publics

        //public PoolSystem m_poolArtifact;
        public SplineContainer[] m_splineContainers;

        #endregion

        /*
         * When get cube
         * GameObject artifact = m_poolArtifact.GetArtifact();
         * artifact.addComponent<ArtifactBehaviour>();
         * ArtifactBehaviour behaviour = artifact.getComponent<ArtifactBehaviour>();
         * behaviour.SetArtifactManager(this);
         * artifact.SetActive(true)
         */
        
        #region Unity API

        private void Awake()
        {
            
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

        #region Privates

        #endregion
    }
}