using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.EnemyScripts
{
    public class PathHandlerBase : MonoBehaviour
    {
        public int curvePoints;
        public float alpha = 0.5f;
        protected LineRenderer lineRenderer;
        protected List<Vector3> positions;
        
        public Vector2 getNextPoint()
        {
            if (positions.Count < 2)
                return transform.position;
            return positions[1];
        }
        
        public bool PointReached()
        {
            Collider2D aircraftCollider = Physics2D.OverlapPoint(positions[1]);
            if (aircraftCollider != null)
            {
                if (aircraftCollider.name == gameObject.name)
                {

                    positions.RemoveAt(1);
                    return true;
                }
            }

            return false;
        }
        
        protected List<Vector3> DrawQuadraticBezierCurve(Vector3 point0, Vector3 point1, Vector3 point2)
        {
            List<Vector3> result = new List<Vector3>();
            float t = 0f;
            Vector3 B = new Vector3(0, 0, 0);
            for (int i = 0; i < curvePoints; i++)
            {
                //B = (1 - t) * (1 - t) * point0 + 2 * (1 - t) * t * point1 + t * t * point2;
                result.Add(B);
                t += (1 / (float)lineRenderer.positionCount);
            }

            return result;
        }

        protected List<Vector3> CatmulRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            List<Vector3> newPoints = new List<Vector3>();
            
            float t0 = 0.0f;
            float t1 = GetT(t0, p0, p1);
            float t2 = GetT(t1, p1, p2);
            float t3 = GetT(t2, p2, p3);
            for (float t=t1; t<t2; t+=((t2-t1)/(float)curvePoints))
            {
                Vector2 A1 = (t1-t)/(t1-t0)*p0 + (t-t0)/(t1-t0)*p1;
                Vector2 A2 = (t2-t)/(t2-t1)*p1 + (t-t1)/(t2-t1)*p2;
                Vector2 A3 = (t3-t)/(t3-t2)*p2 + (t-t2)/(t3-t2)*p3;
		    
                Vector2 B1 = (t2-t)/(t2-t0)*A1 + (t-t0)/(t2-t0)*A2;
                Vector2 B2 = (t3-t)/(t3-t1)*A2 + (t-t1)/(t3-t1)*A3;
		    
                Vector2 C = (t2-t)/(t2-t1)*B1 + (t-t1)/(t2-t1)*B2;
		    
                newPoints.Add(C);
            }
            return newPoints;
        }
        
        float GetT(float t, Vector2 p0, Vector2 p1)
        {
            float a = Mathf.Pow((p1.x-p0.x), 2.0f) + Mathf.Pow((p1.y-p0.y), 2.0f);
            float b = Mathf.Pow(a, alpha * 0.5f);
	   
            return (b + t);
        }
    }
}