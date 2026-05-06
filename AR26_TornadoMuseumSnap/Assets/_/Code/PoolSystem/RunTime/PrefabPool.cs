using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem_RunTime
{
    public class PrefabPool : MonoBehaviour
    {
        #region Publics
        

        #endregion

        #region Main Methods

        public GameObject GetArtefact()
        {
            for (int i = 0; i < _gameObjectsSpawned.Count; i++)
            {
                if (!_gameObjectsSpawned[i].activeInHierarchy)
                {
                    return _gameObjectsSpawned[i];
                }
            }
            return SpawnRandomPrefab();
        }
        
        #endregion

        #region Unity API

        private void Awake()
        {
            SpawnPrefab();
        }

        #endregion
        
        #region Utils


        private GameObject SpawnRandomPrefab()
        {
            int random =  Random.Range(0, _artefactsPrefab.Count);
            
            GameObject obj = Instantiate(_artefactsPrefab[random],transform);
            obj.SetActive(false);
            _gameObjectsSpawned.Add(obj);
            return obj;
        }

        private void SpawnPrefab()
        {
            for (int i = 0; i < _artefactsPrefab.Count; i++)
            {
                GameObject obj = Instantiate(_artefactsPrefab[i],transform);
                obj.SetActive(false);
                _gameObjectsSpawned.Add(obj);
            }
        }
        
        #endregion

        #region Privates
        
        [SerializeField]private List<GameObject> _artefactsPrefab;
        private List<GameObject> _gameObjectsSpawned = new List<GameObject>();
        
        #endregion
    }
}