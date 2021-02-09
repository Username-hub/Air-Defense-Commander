using System;
using DefaultNamespace.EnemyScripts;
using UnityEngine;

namespace DefaultNamespace
{
    public class AircraftScript : MonoBehaviour
    {
        public float speed;

        public PathHandlerBase pathHandlerBase;
        public GameManager gameManager;
        

        
        

        private void Start()
        {
            throw new NotImplementedException();
        }
        
        protected float angle = 0;

        protected void MoveForward(Vector2 toMovePoint)
        {
            Vector2 position = transform.position;
            if (toMovePoint != position)
            {
                float ang = Mathf.Atan2((toMovePoint.y - position.y) , (toMovePoint.x - position.x));
                transform.eulerAngles = new Vector3(0, 0, ang * Mathf.Rad2Deg);
                
                position = Vector2.MoveTowards(position, toMovePoint, (speed * Time.deltaTime));

                angle = ang;
                transform.position = position;
                pathHandlerBase.PointReached();
            }
        }
        
    }
    
}