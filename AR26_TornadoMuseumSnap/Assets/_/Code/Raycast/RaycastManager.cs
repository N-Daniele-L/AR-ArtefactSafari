using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;

public class RaycastManager : MonoBehaviour
{
    #region Publics
    
    public Camera cam;
    public int gridWidth = 48;
    public int gridHeigth = 27;
    public float cellSize = 40f;
    public LayerMask layerMask;
    public List<RaycastHit> results = new List<RaycastHit>(10000);

    #endregion
    
    #region Privates
    private void OnGUI()
    {
        if (GUILayout.Button("Raycast"))
        {
          SendRaycast();  
        }
    }

    #endregion
    
    
    #region Unity API

    public void Awake()
    {
        cam = Camera.main;
    }
    
    
    public void SendRaycast()
    {
        for (int a = 0; a < gridWidth; a++)
        {
            for (int b = 0; b < gridHeigth; b++)
            {
                Vector3 rayDirection = new Vector3(a * cellSize, b * cellSize, 0); 
                Ray r = cam.ScreenPointToRay(rayDirection);
                RaycastHit hit;

                if (Physics.Raycast(r.origin,r.direction, out hit, 20f, layerMask))
                {
                    Debug.DrawLine(r.origin, hit.point, Color.darkGreen,5f); 
                    results.Add(hit);
                    Debug.Log($"Ray hit {hit.collider.gameObject.name} at {hit.point}");
                }
                else 
                { 
                    Debug.DrawLine(r.origin, r.origin + r.direction * 20f, Color.red,5f);
                }
            }
        }
            Debug.Log(results.Count);
    }
            #endregion
}

