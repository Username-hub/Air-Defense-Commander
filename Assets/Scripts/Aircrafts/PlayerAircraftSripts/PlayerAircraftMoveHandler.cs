using UnityEngine;

namespace DefaultNamespace.PlayerAircraftSripts
{
    public class PlayerAircraftMoveHandler : MonoBehaviour
    {
        public float WaitRadius;
        public float rotatinTime;
        public float speed;
        public float angle = 0;
        public PlayerAircraftScript aircraftScript;
        
        public bool MoveForward(Vector2 toMovePoint)
        {
            Vector2 position = transform.position;
            if (toMovePoint != position)
            {
                float ang = Mathf.Atan2((toMovePoint.y - position.y) , (toMovePoint.x - position.x));
                transform.eulerAngles = new Vector3(0, 0, ang * Mathf.Rad2Deg);
                
                position = Vector2.MoveTowards(position, toMovePoint, (speed * Time.deltaTime));

                angle = ang;
                gameObject.transform.localPosition = position;
                return CheckPointReach();
            }

            return false;
        }
        public void MoveInCircle()
        {
            float rotationSpeed = 2 * Mathf.PI * WaitRadius * rotatinTime;
            angle += (rotationSpeed * Time.deltaTime);
            
            
            Vector2 position = transform.position;
            float x = position.x + Mathf.Cos(angle) * WaitRadius;
            float y = position.y + Mathf.Sin(angle) * WaitRadius;
            float ang = Mathf.Atan2((y - position.y) , (x - position.x));
            transform.eulerAngles = new Vector3(0, 0, ang * Mathf.Rad2Deg );


            position.x = x;
            position.y = y;

            transform.position = position;
           
        }
        
        protected bool CheckPointReach()
        {
            if(aircraftScript.getState() == State.FollowPath || aircraftScript.getState() == State.Landing)
            {
                aircraftScript.pathHandlerBase.PointReached();
                return true;
            }
            return false;
        }
    }
}