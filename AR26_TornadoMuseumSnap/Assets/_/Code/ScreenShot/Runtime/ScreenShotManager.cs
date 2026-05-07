using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

namespace ScreenShot.Runtime
{
    public class ScreenShotManager : MonoBehaviour
    {
        #region Unity APi

        private void Awake()
        {
            _mainCamera = Camera.main;
            _rendererTextures =  new List<RenderTexture>();
        }

        private void Update()
        {
            
        }
        

        #endregion

        #region Main Methods

        
        public void TakeScreenShot()
        {
            SetRenderCameraToMainCamera();
            RenderTexture renderTexture = new RenderTexture(480, 270,GraphicsFormat.R8G8B8A8_UNorm,GraphicsFormat.D16_UNorm);
            renderTexture.Create();
            _rendererTextures.Add(renderTexture);
            _renderCamera.targetTexture = renderTexture;
            _renderCamera.enabled = true;
            _renderCamera.Render();
            //SetScreenShotToRawImage();
            _renderCamera.enabled = false;
        }
        
        public void SetScreenShotToRawImage()
        {
            int i = 0;
            foreach (RenderTexture renderTexture in _rendererTextures)
            {
                if (i >= _rawImages.Count) break;
                _rawImages[i].texture = renderTexture;
                _rawImages[i].gameObject.SetActive(true);
                i++;
            }
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
        [SerializeField] private List<RawImage> _rawImages;
        private Camera _mainCamera;
        
        private List<RenderTexture> _rendererTextures;
        private RenderTexture _renderTexture;

        #endregion
    }
}
