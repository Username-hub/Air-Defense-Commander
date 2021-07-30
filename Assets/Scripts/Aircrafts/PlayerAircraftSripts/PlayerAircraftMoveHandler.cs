using UnityEngine;

namespace DefaultNamespace.PlayerAircraftSripts
{
    public class PlayerAircraftMoveHandler : AircraftMoveHandler
    {
        public float WaitRadius;
        public float rotatinTime;
        
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
        
        protected override bool CheckPointReach()
        {
            if((aircraftScript as PlayerAircraftScript).getState() == State.FollowPath || (aircraftScript as PlayerAircraftScript).getState() == State.Landing)
            {
                aircraftScript.pathHandlerBase.PointReached();
                return true;
            }
            return false;
        }
    }
}