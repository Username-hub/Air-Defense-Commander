using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.EnemyScripts;
using DefaultNamespace.PlayerAircraftSripts;
using UnityEngine;

public class PathHandler : PathHandlerBase
{
    
    public int GetPathLength()
    {
        return positions.Count - 1;
    }

    private PlayerAircraftScript aircraftScript;
    public void AddPointToPath(Vector2 position)
    {
        positions.Add(position);
        
    }

    public void ToBasePath(Vector2 position)
    {
        CreateNewPath(transform.position);
        AddPointToPath(position);
        aircraftScript.StopPathMaking();
    }

    public GameObject enemyToChase;
    
    public void ChaseEnemyPath(GameObject enemy)
    {
        enemyToChase = enemy;
        CreateNewPath(transform.position);
        positions.Add(enemyToChase.transform.position);
        aircraftScript.StopPathMaking();
    }

    public Vector3 GetEnemyToChasePos()
    {
        positions[1] = enemyToChase.transform.position;
        return positions[1];
    }
    
    public void CreateNewPath(Vector2 position)
    {
        positions.Clear();
        positions.Add(position);
    }

    public void MakeLandingPath(Vector2 position, Transform takeOfPoint, Transform baseTransform)
    {
        CreateNewPath(position);
        AddPointToPath(takeOfPoint.position);
        AddPointToPath(baseTransform.position);
    }

    private void Start()
    {
        aircraftScript = gameObject.GetComponent<PlayerAircraftScript>();
        positions = new List<Vector3>();
        positions.Add(transform.position);
        
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startColor = Color.gray;
        lineRenderer.endColor = Color.gray;
    }

    private void Update()
    {
        positions[0] = transform.position;
        lineRenderer.positionCount = positions.Count;
        lineRenderer.SetPositions(positions.ToArray());
    }
}
