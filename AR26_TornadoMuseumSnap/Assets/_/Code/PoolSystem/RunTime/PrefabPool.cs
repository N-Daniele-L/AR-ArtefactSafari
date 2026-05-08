using System.Collections.Generic;
using System.Linq;
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
            if (_artefactsSpawned.All(artefact => artefact.activeInHierarchy))
            {
               return SpawnRandomArtefactPrefab();
            }

            _count = 0;
            
            while ((_randomArtefact == _lastArtefactUsed || _artefactsSpawned[_randomArtefact].activeInHierarchy) && _count < 20)

            {
                _count++;
                _randomArtefact = Random.Range(0, _artefactsSpawned.Count);
            }
            
            _lastArtefactUsed = _randomArtefact;
            GameObject obj = _artefactsSpawned[_randomArtefact];
            
            return obj;
        }

        public void RemoveArtefact(GameObject obj) // disable the oldest each time
        {
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
            if (_lastNamedObject == null)
            {
                GameObject obj = Instantiate(_artefactsPrefab[random],transform);
                obj.SetActive(false);
                _lastNamedObject = obj.name;
                _artefactsSpawned.Add(obj);
                return obj; 
            }
            else
            {
                GameObject obj = Instantiate(_artefactsPrefab[random],transform);
                obj.SetActive(false);
                if (_lastNamedObject.Equals(obj.gameObject.name))
                {
                    if (random == _artefactsPrefab.Count-1) random = 0;
                    else random++;
                    obj = Instantiate(_artefactsPrefab[random],transform);
                }
                _lastNamedObject = obj.name;
                _artefactsSpawned.Add(obj);
                return obj; 
            }
        }

        private void SpawnPrefab()
        {
            for (int i = 0; i < _artefactsPrefab.Count; i++)
            {
                GameObject obj = Instantiate(_artefactsPrefab[i],transform);
                obj.SetActive(false);
                obj.name = ChangeName(obj);
                _artefactsSpawned.Add(obj);
            }
        }

        private string ChangeName(GameObject obj)
        {
            string newName = (obj.transform.GetChild(0)).name;
            newName = newName.Replace("(Clone)", "");
            newName = newName.Replace("P_", "");
            return newName;
        }
        
        #endregion

        #region Privates
        
        [SerializeField]private List<GameObject> _artefactsPrefab;
        private List<GameObject> _artefactsSpawned = new List<GameObject>();
        private string _lastNamedObject;
        private int _randomArtefact;
        private int _lastArtefactUsed;
        private int _count = 0;

        #endregion
    }
}