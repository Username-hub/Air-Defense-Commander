using UnityEngine;

namespace DefaultNamespace
{
    public class AircraftMoveHandler : MonoBehaviour
    {
        public AircraftScript aircraftScript;
        public float speed;
        public float angle = 0;
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

        protected virtual bool CheckPointReach()
        {
            return aircraftScript.pathHandlerBase.PointReached();
        }
    }
}