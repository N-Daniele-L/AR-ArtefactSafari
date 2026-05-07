using UnityEngine;
using UnityEngine.Splines;

namespace Artifact.Runtime
{
    [RequireComponent(typeof(SplineAnimate))]
    public class ArtifactBehaviour : MonoBehaviour
    {
        #region Publics
        
        [Header("Spline Component")]
        public SplineAnimate m_splineAnimate;
        public ArtifactManager m_artifactManager;

        #endregion

        #region Unity API

        private void Awake()
        {
            m_splineAnimate = GetComponent<SplineAnimate>();
        }

        private void Start()
        {
            SetupSplineAnimation();
        }

        private void Update()
        {
            if(!_hasBeenPhotographied) IdleMovement();
            if(_hasBeenPhotographied) ReactionMovement();
        }

        #endregion

        #region Main Methods

        public void SetArtifactManager(ArtifactManager artifactManager)
        {
            m_artifactManager = artifactManager;
        }
        
        #endregion

        #region Utils

        private void SetupSplineAnimation()
        {
            float offset = Random.Range(0, 1);
            SplineContainer container = m_artifactManager.GetRandomSplineContainer();
            m_splineAnimate.Container = container;
            m_splineAnimate.AnimationMethod = SplineAnimate.Method.Speed;
            m_splineAnimate.MaxSpeed = _speed;
            m_splineAnimate.Loop = SplineAnimate.LoopMode.Loop;
            m_splineAnimate.Alignment = SplineAnimate.AlignmentMode.None;
            m_splineAnimate.StartOffset = offset; 
            m_splineAnimate.Play();
        }

        private void IdleMovement()
        {
            m_splineAnimate.MaxSpeed = Mathf.Lerp(m_splineAnimate.MaxSpeed, _speed, Time.deltaTime);
            transform.Rotate(Vector3.one, _angleRotationPerSecond * Time.deltaTime);
        }

        private void ReactionMovement()
        {
            m_splineAnimate.MaxSpeed = Mathf.Lerp(m_splineAnimate.MaxSpeed, _speed * 2f, Time.deltaTime);
        }

        #endregion

        #region Privates
        
        [Header("Params")] [SerializeField]
        [Range(0,100)]private float _speed = 1.5f;
        [SerializeField] private float _angleRotationPerSecond = 180f;
        [SerializeField] private bool _hasBeenPhotographied = false;

        #endregion
    }
}