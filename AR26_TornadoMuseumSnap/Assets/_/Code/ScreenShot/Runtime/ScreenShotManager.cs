using Data.Runtime;
using ScoreManager.Runtime;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace ScreenShot.Runtime
{
    public class ScreenShotManager : MonoBehaviour
    {
        #region Unity APi

        private void Awake()=> _mainCamera = Camera.main;
        

        #endregion

        #region Main Methods

        
        public void TakeScreenShot()
        {
            SetRenderCameraToMainCamera();
            RenderTexture renderTexture = new RenderTexture(480, 270,GraphicsFormat.R8G8B8A8_UNorm,GraphicsFormat.D16_UNorm);
            renderTexture.Create();
            
            _renderCamera.targetTexture = renderTexture;
            _renderCamera.enabled = true;
            _renderCamera.Render();
            
            //insert raycast here
            int raycastHit = 10;
            
            
            ScreenShotData shotData = new ScreenShotData(renderTexture, raycastHit);
            _scoreManager.GetScreenshot(shotData);
            _renderCamera.enabled = false;
        }
        

        #endregion

        #region Utils

        private void SetRenderCameraToMainCamera()
        {
            _renderCamera.transform.position = _mainCamera.transform.position;
            _renderCamera.transform.rotation = _mainCamera.transform.rotation;
        }
        

        #endregion
        
        #region Private

        [SerializeField] private Camera _renderCamera;
        [SerializeField] private ScoreFromScreenshotManager _scoreManager;
        private Camera _mainCamera;

        #endregion
    }
}
