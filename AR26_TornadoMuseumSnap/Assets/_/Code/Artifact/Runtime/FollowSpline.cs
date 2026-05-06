using UnityEngine;
using UnityEngine.Splines;

public class FollowSpline : MonoBehaviour
{
    #region Publics
    
    public SplineContainer m_splineContainer;
    public GameObject Cube;

    #endregion
    
    #region Unity API

    private void Awake()
    {
        m_splineContainer = GetComponent<SplineContainer>();
    }

    private void Update()
    {
        for (int i = 0; i < m_splineContainer.Splines.Count; i++)
        {
            var x = m_splineContainer.Spline[0];
        }
    }
    
    #endregion
}
