using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PoolSystem.RunTime
{
    public class DebugPoolSystem : MonoBehaviour
    {
        #region Publics
        #endregion

        #region Main Methods
        #endregion

        #region Unity API

        private void Awake()
        {
            _mouseCLick = new InputAction(binding: "<Mouse>/LeftButton");
        }

        private void OnEnable()
        {
            _mouseCLick.Enable();
            _mouseCLick.performed += FireRaycastAtMousePositon;
        }

        

        private void OnDisable()
        {
            _mouseCLick.Disable();
            _mouseCLick.performed -= FireRaycastAtMousePositon;
        }
        

        public void TestGetArtefactOn()
        {
            _artefactsPrefab.GetArtefact().SetActive(true);
        }
        
        #endregion

        #region Utils
        
        private void FireRaycastAtMousePositon(InputAction.CallbackContext context)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject obj = hit.collider.gameObject;
                _artefactsPrefab.RemoveArtefact(obj);
            }
            
        }
        
        #endregion

        #region Privates
        
        [SerializeField]private PrefabPool _artefactsPrefab;

        private InputAction _mouseCLick;

        #endregion
    }
}
