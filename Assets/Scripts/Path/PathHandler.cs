using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathHandler : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public PathHandler()
    {
        
    }
    private List<Vector3> positions;

    public void AddPointToPath(Vector2 position)
    {
        positions.Add(position);
        
    }

    public void CreateNewPath(Vector2 position)
    {
        positions.Clear();
        positions.Add(position);
    }

    public Vector2 getNextPoint()
    {
        if (positions.Count < 2)
            return transform.position;
        return positions[1];
    }

    public void PointReached()
    {
        Collider2D aircraftCollider = Physics2D.OverlapPoint(positions[1]);
        if (aircraftCollider != null)
        {
            if (aircraftCollider.name == gameObject.name)
            {

                positions.RemoveAt(1);
            }
        }
    }

    private void Start()
    {
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
