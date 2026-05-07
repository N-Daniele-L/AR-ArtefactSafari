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
            if (_renderCamera.enabled)
            {
                _renderCamera.enabled = false;
                SetScreenShotToRawImage();
                _renderCamera.targetTexture = null;
            }
        }
        

        #endregion

        #region Main Methods

        public void TakeScreenShot()
        {
            SetRenderCameraToMainCamera();
            RenderTexture renderTexture = new RenderTexture(480, 270,GraphicsFormat.B8G8R8A8_UNorm,GraphicsFormat.D32_SFloat_S8_UInt);
            _rendererTextures.Add(renderTexture);
            _renderCamera.targetTexture = renderTexture;
            _renderCamera.enabled = true;
        }

        

        #endregion

        #region Utils

        private void SetRenderCameraToMainCamera()
        {
            _renderCamera.transform.position =  _mainCamera.transform.position;
            _renderCamera.transform.rotation = _mainCamera.transform.rotation;
        }

        private void SetScreenShotToRawImage()
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
        
        #region Private

        [SerializeField] private Camera _renderCamera;
        private Camera _mainCamera;
        
        [SerializeField] private List<RenderTexture> _rendererTextures;
        private RenderTexture _renderTexture;
        [SerializeField] private List<RawImage> _rawImages;

        #endregion
    }
}
