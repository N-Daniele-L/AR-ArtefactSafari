using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;

public class RaycastManager : MonoBehaviour
{
    
    [SerializeField] private int rayCount = 10;
    [SerializeField] private float spacing = 0.1f;
    [SerializeField] private float rayLength = 10f;

    
    public LayerMask m_touchableTarget;
    
    private void Start()
    {
        // Allocate raycast commands and results
        var commands = new NativeArray<RaycastCommand>(rayCount, Allocator.TempJob);
        var results = new NativeArray<RaycastHit>(rayCount, Allocator.TempJob);

        // Set up raycast commands
        for (int i = 0; i < rayCount; i++)
        {
            for (int j = 10; j < rayCount; j--)
            {
                Vector3 origin = transform.position;
                Vector3 direction = transform.TransformVector(i * spacing, j * spacing, 5);
                commands[i] = new RaycastCommand(origin, direction, QueryParameters.Default);
                Debug.DrawRay(commands[i].from, commands[i].direction * rayLength, Color.red, 20f);
            }
        }

        // Schedule and complete batch
        JobHandle handle = RaycastCommand.ScheduleBatch(commands, results, 1, default);
        handle.Complete();

        // Process results
        for (int i = 0; i < rayCount; i++)
        {
            if (results[i].collider != null)
            {
                Debug.DrawLine(commands[i].from, results[i].point, Color.green, 20f);
                Debug.Log($"Ray {i} hit {results[i].collider.name} at {results[i].point}");
            }
            else
            {
                Debug.DrawRay(commands[i].from, commands[i].direction * rayLength, Color.red, 20f);
                Debug.Log($"Ray {i} missed.");
            }
        }

        // Dispose memory
        results.Dispose();
        commands.Dispose();
    }
}


/*
#region Publics

public float m_rayDistance = 5f;
public LayerMask m_touchableTarget;
public QueryTriggerInteraction m_triggerInteraction;
public List<RaycastCommand> m_commands = new List<RaycastCommand>();
#endregion


#region Unity API

private void Update()
{
    //Check the postion
    Vector3 origine =  transform.position;
    Vector3 direction = transform.forward;


    bool hit = Physics.Raycast(origine,
        direction *  m_rayDistance,
        out RaycastHit hitInfo,
        m_rayDistance,
        m_touchableTarget,
        m_triggerInteraction);

    Color debugcolor = Color.red;

    if (hit)
    {
        debugcolor = Color.green;
    }
    Debug.DrawRay(origine, direction * m_rayDistance, debugcolor);
}

#endregion

*/