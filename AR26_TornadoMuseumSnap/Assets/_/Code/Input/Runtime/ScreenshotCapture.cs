using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace Input.Runtime
{
    public class ScreenshotCapture : MonoBehaviour
    {
        #region Publics

        public InputActionReference _ScreenshotCapture;
        public Button _CaptureButton;

        #endregion

        #region Unity API

        private void OnEnable()
        {

            _CaptureButton.onClick.AddListener(OnScreenshotCaptureActionPerformed);
        }
        

        private void OnDisable()
        {
            _CaptureButton.onClick.RemoveListener(OnScreenshotCaptureActionPerformed);
           }

        #endregion

        #region Main methods

        #endregion

        #region Utils
        private void OnScreenshotCaptureActionPerformed()
        {
            Debug.Log("Screenshot captured");
        }

        #endregion

        #region Privates

        #endregion
    }
}
