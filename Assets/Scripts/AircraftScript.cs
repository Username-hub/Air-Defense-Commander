using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class AircraftScript : MonoBehaviour
    {
        public float speed;
        private PathHandler pathHandler;
        private State state;

        private PathMakingState pathMakingState;
        public void StartPathMaking()
        {
            pathHandler.CreateNewPath(transform.position);
            pathMakingState = PathMakingState.MakingPath;
        }
        private void Start()
        {
            pathMakingState = PathMakingState.None;
            state = State.Wait;
            pathHandler = GetComponent<PathHandler>();
            angle = 90;
        }
        
        private void Update()
        {
            if (pathMakingState == PathMakingState.MakingPath)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.touches[0];
                    Vector3 wp= Camera.main.ScreenToWorldPoint(touch.position);
                    Vector2 touchPos = new Vector2(wp.x, wp.y);
                    Collider2D collider2D = TouchControlHandler.GetOnVector2Collider(touchPos);
                    if (collider2D != null)
                    {
                        if (collider2D.tag == "Base")
                        {
                            pathHandler.ToBasePath(touchPos);
                        }
                    }
                    else
                    {
                        pathHandler.AddPointToPath(touchPos);
                    }
                }
                else
                {
                    pathMakingState = PathMakingState.None;
                }
            }
            if (state == State.FollowPath)
            {
                if (pathHandler.GetPathLength() == 0)
                {
                    state = State.Wait;
                }
                else
                {
                    MoveForward();
                }
            }else if (state == State.Wait)
            {
                if (pathHandler.GetPathLength() > 0)
                    state = State.FollowPath;
                MoveInCircle();
            }
        }

        public void StopPathMaking()
        {
            pathMakingState = PathMakingState.None;
        }
        private void MoveForward()
        {
            Vector2 toMovePoint = pathHandler.getNextPoint();
            Vector2 position = transform.position;
            if (toMovePoint != position)
            {
                float ang = Mathf.Atan2((toMovePoint.y - position.y) , (toMovePoint.x - position.x));
                transform.eulerAngles = new Vector3(0, 0, ang * Mathf.Rad2Deg);
                
                position = Vector2.MoveTowards(position, toMovePoint, (speed * Time.deltaTime));

                angle = ang;
                transform.position = position;
                pathHandler.PointReached();
            }
        }

        public float WaitRadius;
        public float rotatinTime;
        private float angle = 0;
        private void MoveInCircle()
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
    }
    enum State
    {
        Wait,
        FollowPath,
        FollowAircraft
    }

    enum PathMakingState
    {
        MakingPath,
        None
    }
}