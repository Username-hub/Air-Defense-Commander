using System;
using System.Collections.Generic;
using DefaultNamespace.EnemyScripts;
using UnityEngine;

namespace DefaultNamespace.Path
{
    public class EnemyPathHandler : PathHandlerBase
    {

        public int stepsToAim;

        private EnemyAircraftScript enemyAircraftScript;
        
        private void Start()
        {
            enemyAircraftScript = gameObject.GetComponent<EnemyAircraftScript>();
            positions = new List<Vector3>();
            positions.Add(transform.position);
            lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            positions[0] = transform.position;
            lineRenderer.positionCount = positions.Count;
            lineRenderer.SetPositions(positions.ToArray());
        }

        public void BuildAfterBombingPath(Vector3 aim)
        {
            positions.Clear();
            positions.Add(transform.position);
            positions.Add(aim);
            lineRenderer.positionCount = positions.Count;
            lineRenderer.SetPositions(positions.ToArray());
        }
        public void BuildPath(Vector3 aim)
        {
            List<Vector3> path = new List<Vector3>();
            path.Add(transform.position);
            path.Add(aim);
            
            for (int i = 0; i < stepsToAim - 2; i++)
            {
                Vector2 midPoint = FindNextHalfPoint(path[i], path[path.Count - 1]);
                path.Insert(i + 1, midPoint);
            }
            positions.AddRange(path);
            positions.RemoveAt(1);
            /*for (int i = 0; i < path.Count - 1; i+=2)
            {
                positions.AddRange(DrawQuadraticBezierCurve(path[i],path[i+1],path[i+2]));
            }*/
            /*positions.RemoveAt(0);
            int len = path.Count;
            positions.Add(path[0]);
            positions.Add(path[1]);
            for (int i = 0; i < len-3; i+=3)
            {
                positions.AddRange(CatmulRom(path[i],path[i+1],path[i+2],path[i+3]));
            }
            positions.Add(path[len - 2]);
            positions.Add(path[len - 1]);
            */
            //positions.AddRange(CatmulRom(path[len - 3],path[len - 2],path[len - 1],path[len - 1]));
            lineRenderer.positionCount = positions.Count;
            lineRenderer.SetPositions(positions.ToArray());
        }

        private Vector2 FindNextHalfPoint(Vector2 A, Vector2 B)
        {
            float ABDist = Vector2.Distance(A, B);
            float ACDist = Mathf.Cos(45.0f * Mathf.Deg2Rad) * ABDist;
            
            
            float ang;
            float x, y;
            /*     B
             *   |*
             * A
             */
            if (A.x < B.x & A.y < B.y)
            {
                float d = Mathf.Abs(A.x - B.x);
                ang = Mathf.Acos(d / ABDist) - (45.0f * Mathf.Deg2Rad);
                x = A.x + Mathf.Cos(ang) * ACDist;
                y = A.y + Mathf.Sin(ang) * ACDist;
                
            }
            /*  B
             *   \*
             *    A
             */
            else if(A.x > B.x & A.y < B.y)
            {
                float d = Mathf.Abs(A.y - B.y);
                ang = Mathf.Acos(d / ABDist) + (45.0f * Mathf.Deg2Rad);
                x = A.x + Mathf.Cos(ang) * ACDist;
                y = A.y + Mathf.Sin(ang) * ACDist;
                
            }
            /*  A
             *  *\
             *    B
             */
            else if (A.x < B.x & A.y > B.y)
            {
                float d = Mathf.Abs(A.y - B.y);
                ang = (225.0f * Mathf.Deg2Rad) + Mathf.Acos(d / ABDist);
                x = A.x + Mathf.Cos(ang) * ACDist; 
                y = A.y + Mathf.Sin(ang) * ACDist;
            }
            /*    A
             *  *|
             *  B 
             */
            else
            {
                float d = Mathf.Abs(A.y - B.y);
                ang = (235.0f * Mathf.Deg2Rad) - Mathf.Acos(d / ABDist);
                x = A.x + Mathf.Cos(ang) * ACDist;
                y = A.y + Mathf.Sin(ang) * ACDist;
            }
            
            return new Vector2(x, y);
        }
    }
}