using DefaultNamespace.AirBaseScripts;
using DefaultNamespace.EnemyScripts;
using UnityEngine;

namespace DefaultNamespace.PlayerAircraftSripts
{
    public class PlayerAircraftScript : AircraftScript
    {
        public float maxFuel;
        public float fuelConsumptionRate;
        private float currentFuel;
        private State state;
        
        private PathMakingState pathMakingState;

        public float maxSpeed;
        public AircraftData aircraftData;
        private void Start()
        {
            speed = maxSpeed;
            currentFuel = maxFuel;
            pathMakingState = PathMakingState.None;
            state = State.Wait;
            pathHandlerBase = GetComponent<PathHandler>();
            angle = 90;
        }

        private void Update()
        {
            if (gameManager.gameState != GameManager.GameState.paused)
            {
                //Path making
                if (pathMakingState == PathMakingState.MakingPath)
                {
                    TouchPathMaking();
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
                }
                else if (state == State.Wait)
                {
                    if ((pathHandlerBase as PathHandler).GetPathLength() > 0)
                        state = State.FollowPath;
                    MoveInCircle();
                }
                else if (state == State.FollowAircraft)
                {
                    MoveForward((pathHandlerBase as PathHandler).GetEnemyToChasePos());
                }
                else if (state == State.CloseFollow)
                {
                    if ((pathHandlerBase as PathHandler).enemyToChase != null)
                        MoveForward((pathHandlerBase as PathHandler).GetEnemyToChasePos());
                    else
                    {
                        state = State.FollowPath;
                        speed = maxSpeed;
                    }
                }
                else if (state == State.ToBaseMove || state == State.Landing)
                {
                    MoveForward(pathHandlerBase.getNextPoint());
                }

                UpdateAircrafData();
                UpdateAircrafUI();
            }
        }

        public void GoToWaitState()
        {
            (pathHandlerBase as PathHandler).CleatPath();
            state = State.Wait;
        }

        public void UpdateAircrafData()
        {
            aircraftData.HP = currentHealth;
        }
        public void enemyInSootRange(GameObject enemyGameObjet)
        {
            if (state == State.FollowAircraft)
            {
                if (enemyGameObjet == (pathHandlerBase as PathHandler).enemyToChase)
                {
                    EnemyAircraftScript enemyAircraftScript =
                        (pathHandlerBase as PathHandler).enemyToChase.GetComponent<EnemyAircraftScript>();
                    speed = enemyAircraftScript.speed;
                    (pathHandlerBase as PathHandler).enemyToChase = enemyAircraftScript.enemyTail;
                    state = State.CloseFollow;
                }
            }
        }

        public void BaseInRange(Collider2D collider2D)
        {
            if (state == State.ToBaseMove)
            {
                AirBaseScript airBaseScripts = collider2D.gameObject.GetComponent<AirBaseScript>();
                (pathHandlerBase as PathHandler).MakeLandingPath(transform.position, airBaseScripts.GetLandingPoint(),airBaseScripts.gameObject.transform);
                state = State.Landing;
            }else if (state == State.Landing & (pathHandlerBase as PathHandler).GetPathLength() < 2)
            {
                AirBaseScript airBaseScripts = collider2D.gameObject.GetComponent<AirBaseScript>();
                airBaseScripts.AircraftLanding(this, aircraftData);
            }
        }
        private void TouchPathMaking()
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
                        state = State.ToBaseMove;
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
        
        public void StartPathMaking()
        {
            (pathHandlerBase as PathHandler).CreateNewPath(transform.position);
            pathMakingState = PathMakingState.MakingPath;
            state = State.FollowPath;
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

        protected override bool CheckPointReach()
        {
            if(state == State.FollowPath || state == State.Landing)
            {
                pathHandlerBase.PointReached();
                return true;
            }
            return false;
        }

    }

    enum State
    {
        Wait,
        FollowPath,
        FollowAircraft,
        CloseFollow,
        ToBaseMove,
        Landing
    }

    enum PathMakingState
    {
        MakingPath,
        None
    }
    
    
}