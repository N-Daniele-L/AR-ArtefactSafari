using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;

public class RaycastManager : MonoBehaviour
{
    #region Publics
    
    public int gridWidth = 192;
    public int gridHeigth = 108;
    public float cellSize = 0.05f;
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
    public void SendRaycast()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeigth; z++)
            {
                Vector3 rayorigin = new Vector3(x * cellSize, z * cellSize, 0); 
                rayorigin += transform.position; 
                Vector3 rayDirection = Vector3.forward; 

                RaycastHit hit;

                if (Physics.Raycast(rayorigin, rayDirection, out hit, 20f, layerMask))
                {
                    Debug.DrawLine(rayorigin, hit.point, Color.darkGreen,5f); 
                    results.Add(hit);
                    Debug.Log($"Ray hit {hit.collider.gameObject.name} at {hit.point}");
                }
                else 
                { 
                    Debug.DrawLine(rayorigin, rayorigin + rayDirection * 20f, Color.red,5f);
                }
            }
        }
            Debug.Log(results.Count);
    }
            #endregion
}

