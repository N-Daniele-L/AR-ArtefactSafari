using System;
using UnityEngine;

namespace Data.Runtime
{
    [Serializable]
    public class MaxHitForGameObjectData
    {
        public GameObject m_obj;
        public int m_hit;
    
        public MaxHitForGameObjectData(GameObject obj, int hit)
        {
            m_obj = obj;
            m_hit = hit;
        }
    }
}
