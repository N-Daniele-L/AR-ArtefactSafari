using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace PoolSystem_RunTime
{
    public class PrefabSpawn : MonoBehaviour
    {
        #region Publics
        

        public List<GameObject> m_artefactsPrefab;
        

        #endregion

        #region Main Methods
        
        #endregion

        #region Unity API

        private void Awake()
        {
            SpawnPrefab();
        }

        #endregion
        #region Utils

        private void SpawnPrefab()
        {
            for (int i = 0; i < m_artefactsPrefab.Count; i++)
            {
                GameObject obj = Instantiate(m_artefactsPrefab[i]);
                obj.SetActive(false);
            }
        }
        
        #endregion

        #region Privates

        #endregion
    }
}