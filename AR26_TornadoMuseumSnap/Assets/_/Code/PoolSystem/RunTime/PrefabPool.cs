using System.Collections.Generic;
using UnityEngine;

namespace PoolSystem.RunTime
{
    public class PrefabPool : MonoBehaviour
    {
        #region Publics
        

        #endregion

        #region Main Methods

        public GameObject GetArtefact() // active the first one of the list
        {
            for (int i = 0; i < _artefactsSpawned.Count; i++)
            {
                if (!_artefactsSpawned[i].activeInHierarchy)
                {
                    return _artefactsSpawned[i];
                }
            }
            return SpawnRandomArtefactPrefab(); // if all actived, spawn a new objet random of the list
        }

        public void RemoveArtefact(GameObject obj) // disable the oldest each time
        {
            // for (int i = 0; i < _gameObjectsSpawned.Count; i++)
            // {
            //     if (_gameObjectsSpawned[i].activeInHierarchy)
            //     {
            //         _gameObjectsSpawned[i].SetActive(false);
            //         break;
            //     }
            // }
            
            obj.SetActive(false);
        }
        
        #endregion

        #region Unity API

        private void Awake()
        {
            SpawnPrefab();
        }

        #endregion
        
        #region Utils


        private GameObject SpawnRandomArtefactPrefab() // random spawn
        {
            int random =  Random.Range(0, _artefactsPrefab.Count);
            
            GameObject obj = Instantiate(_artefactsPrefab[random],transform);
            obj.SetActive(false);
            _artefactsSpawned.Add(obj);
            return obj;
        }

        private void SpawnPrefab()
        {
            for (int i = 0; i < _artefactsPrefab.Count; i++)
            {
                GameObject obj = Instantiate(_artefactsPrefab[i],transform);
                obj.SetActive(false);
                _artefactsSpawned.Add(obj);
            }
        }
        
        #endregion

        #region Privates
        
        [SerializeField]private List<GameObject> _artefactsPrefab;
        private List<GameObject> _artefactsSpawned = new List<GameObject>();
        
        #endregion
    }
}