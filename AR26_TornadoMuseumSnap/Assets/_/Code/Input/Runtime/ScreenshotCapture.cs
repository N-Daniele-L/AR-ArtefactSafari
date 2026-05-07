using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace Input.Runtime
{
    public class ScreenshotCapture : MonoBehaviour
    {
        #region Publics

        public InputActionReference m_ScreenshotCapture;
        public Button m_CaptureButton;

        #endregion

        
        #region Unity API

        private void OnEnable()
        {
            m_CaptureButton.onClick.AddListener(OnScreenshotCaptureActionPerformed);
        }

        private void Start()
        {
            _isDelayScreenshotInSecondsActive = true;
        }

        private void Update()
        {
            if (_isDelayScreenshotInSecondsActive)
            {
                _timer +=  Time.deltaTime;
                if (_timer >= _delayScreenshotInSeconds)
                {
                    _isDelayScreenshotInSecondsActive = false;
                    m_CaptureButton.interactable = true;  
                    _timer = 0;
                }
            }
        }
        private void OnDisable()
        {
            m_CaptureButton.onClick.RemoveListener(OnScreenshotCaptureActionPerformed);
           }

        #endregion

        
        #region Main methods

        #endregion

        
        #region Utils
        private void OnScreenshotCaptureActionPerformed()
        {
            _isDelayScreenshotInSecondsActive = true;
            m_CaptureButton.interactable = false;
        }

        #endregion

        
        #region Privates

        [SerializeField] private float _delayScreenshotInSeconds;
        private float _timer;
        [SerializeField] private bool _isDelayScreenshotInSecondsActive;

        #endregion
    }
}
