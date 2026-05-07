using UnityEngine;

namespace Data.Runtime
{
    public class ScreenShotData
    {
        public RenderTexture m_shotTexture;
        public int m_numberOfRaycastHit;
        public GameObject m_objectHit;
        public int m_screenShotScore;

        public ScreenShotData(RenderTexture rt, int raycast)
        {
            m_shotTexture = rt;
            m_numberOfRaycastHit = raycast;
            m_objectHit = null;
            m_screenShotScore = 0;
        }

        public ScreenShotData(RenderTexture rt, int raycast, GameObject go)
        {
            m_shotTexture = rt;
            m_numberOfRaycastHit = raycast;
            m_objectHit = go;
            m_screenShotScore = 0;
        }
    }
}