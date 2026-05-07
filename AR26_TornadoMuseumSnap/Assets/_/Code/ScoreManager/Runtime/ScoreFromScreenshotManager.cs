using System.Collections.Generic;
using System.Linq;
using Data.Runtime;
using UnityEngine;

namespace ScoreManager.Runtime
{
    public class ScoreFromScreenshotManager : MonoBehaviour
    {
        #region Unity API

        private void Awake()
        {
            Initialise();
        }

        #endregion
        
        #region Main Methods

        public void GetScreenshot(ScreenShotData data)
        {
            data.m_screenShotScore = CalculateScore(data);
            _screenShotDatas.Add(data);
            _screenShotDatas = _screenShotDatas.OrderByDescending(x => x.m_screenShotScore).ToList();
        }

        public List<ScreenShotData> EndScoreData()
        {
            return _screenShotDatas;
        }

        #endregion

        #region Utils

        private void Initialise()
        {
            _screenShotDatas =  new List<ScreenShotData>();
        }

        private int CalculateScore(ScreenShotData data)
        {
            return (int)(data.m_numberOfRaycastHit * _pointWeight);
        }

        #endregion
        
        #region Private

        private List<ScreenShotData> _screenShotDatas;

        [SerializeField] private float _pointWeight;
        [SerializeField] private int _numberOfBest;


        #endregion
    }
}
