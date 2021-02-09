using DefaultNamespace.EnemyScripts;
using UnityEngine;

namespace DefaultNamespace.PlayerAircraftSripts
{
    public class PlayerAircraftScript : AircraftScript
    {

        private State state;
        
        private PathMakingState pathMakingState;
        private void Start()
        {
            pathMakingState = PathMakingState.None;
            state = State.Wait;
            pathHandlerBase = GetComponent<PathHandler>();
            angle = 90;
        }
        private void Update()
        {
            //Path making
            if (pathMakingState == PathMakingState.MakingPath)
            {
                if (Input.touchCount > 0)
                {
                    gameManager.isPathMaking = true;
                    Touch touch = Input.touches[0];
                    Vector3 wp= Camera.main.ScreenToWorldPoint(touch.position);
                    Vector2 touchPos = new Vector2(wp.x, wp.y);
                    Collider2D collider2D = TouchControlHandler.GetOnVector2Collider(touchPos);
                    if (collider2D != null)
                    {
                        if (collider2D.tag == "Base")
                        {
                            (pathHandlerBase as PathHandler).ToBasePath(touchPos);
                        }

                        if (collider2D.tag == "Enemy")
                        {
                            (pathHandlerBase as PathHandler).ChaseEnemyPath(collider2D.gameObject);
                            state = State.FollowAircraft;
                        }
                    }
                    else
                    {
                        (pathHandlerBase as PathHandler).AddPointToPath(touchPos);
                    }
                }
                else
                {
                    gameManager.isPathMaking = false;
                    pathMakingState = PathMakingState.None;
                }
            }
            //Move
            if (state == State.FollowPath)
            {
                if ((pathHandlerBase as PathHandler).GetPathLength() == 0)
                {
                    state = State.Wait;
                }
                else
                {
                    MoveForward(pathHandlerBase.getNextPoint());
                }
            }else if (state == State.Wait)
            {
                if ((pathHandlerBase as PathHandler).GetPathLength() > 0)
                    state = State.FollowPath;
                MoveInCircle();
            }
            else if(state == State.FollowAircraft)
            {
                MoveForward((pathHandlerBase as PathHandler).GetEnemyToChasePos());
            }
        }
        public void StartPathMaking()
        {
            (pathHandlerBase as PathHandler).CreateNewPath(transform.position);
            pathMakingState = PathMakingState.MakingPath;
        }
        
        public void StopPathMaking()
        {
            pathMakingState = PathMakingState.None;
            gameManager.isPathMaking = false;
        }
        
        public float WaitRadius;
        public float rotatinTime;
        
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