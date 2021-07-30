using DefaultNamespace.AirBaseScripts;
using DefaultNamespace.EnemyScripts;
using UnityEngine;

namespace DefaultNamespace.PlayerAircraftSripts
{
    public class PlayerAircraftScript : AircraftScript
    {

        private State state;

        public State getState()
        {
            return state;
        }
        
        private PathMakingState pathMakingState;

        public float maxSpeed;
        public AircraftData aircraftData;
        
        private void Start()
        {
            aircraftMoveHandler.speed = maxSpeed;
            currentFuel = maxFuel;
            pathMakingState = PathMakingState.None;
            state = State.Wait;
            pathHandlerBase = GetComponent<PathHandler>();
            aircraftMoveHandler.angle = 90;
        }

        private void Update()
        {
            if (gameManager.gameState != GameManager.GameState.paused)
            {
                OnGameNotPausedUpdate();
            }
                
        }

        private void OnGameNotPausedUpdate()
        {
            //Path making
            if (pathMakingState == PathMakingState.MakingPath)
            {
                TouchPathMaking();
            }
            //Move
            if (state == State.FollowPath)
            {
                StateFollowPassUpdate();
            }
            else if (state == State.Wait)
            {
                StateWaitUpdate();
            }
            else if (state == State.FollowAircraft)
            {
                StateFollowAircraftUpdate();
            }
            else if (state == State.CloseFollow)
            {
                StateCloseFollowUpdate();
            }
            else if (state == State.ToBaseMove || state == State.Landing)
            {
                aircraftMoveHandler.MoveForward(pathHandlerBase.getNextPoint());
            }

            currentFuel -= fuelConsumptionRate * Time.deltaTime;
                
            UpdateAircrafData();
            UpdateAircrafUI();
        }

        private void StateFollowPassUpdate()
        {
            if ((pathHandlerBase as PathHandler).GetPathLength() == 0)
            {
                state = State.Wait;
            }
            else
            {
                aircraftMoveHandler.MoveForward(pathHandlerBase.getNextPoint());
            }
        }

        public void StateWaitUpdate()
        {
            if ((pathHandlerBase as PathHandler).GetPathLength() > 0)
                state = State.FollowPath;
            (aircraftMoveHandler as PlayerAircraftMoveHandler).MoveInCircle();
        }

        public void StateFollowAircraftUpdate()
        {
            aircraftMoveHandler.MoveForward((pathHandlerBase as PathHandler).GetEnemyToChasePos());
        }

        private void StateCloseFollowUpdate()
        {
            if (((PathHandler) pathHandlerBase).enemyToChase != null)
                aircraftMoveHandler.MoveForward((pathHandlerBase as PathHandler).GetEnemyToChasePos());
            else
            {
                state = State.FollowPath;
                aircraftMoveHandler.speed = maxSpeed;
            }
        }
        public void GoToWaitState()
        {
            (pathHandlerBase as PathHandler).CleatPath();
            state = State.Wait;
        }

        public void MoveAircraftForward()
        {
            
        }

        public void UpdateAircrafData()
        {
            aircraftData.HP = currentHealth;
            aircraftData.fuel = currentFuel;
        }
        
        public void EnemyInSootRange(GameObject enemyGameObjet)
        {
            if (state == State.FollowAircraft)
            {
                if (enemyGameObjet == (pathHandlerBase as PathHandler).enemyToChase)
                {
                    EnemyAircraftScript enemyAircraftScript =
                        (pathHandlerBase as PathHandler).enemyToChase.GetComponent<EnemyAircraftScript>();
                    aircraftMoveHandler.speed = enemyAircraftScript.GetAircraftSpeed();
                    (pathHandlerBase as PathHandler).enemyToChase = enemyAircraftScript.enemyTail;
                    state = State.CloseFollow;
                }
            }
        }

        public void BaseInRange(Collider2D collider2D)
        {
            if (state == State.ToBaseMove)
            {
                BaseInRangeStateToBaseMove(collider2D);   
            }else if (state == State.Landing & (pathHandlerBase as PathHandler).GetPathLength() < 2)
            {
                AirBaseScript airBaseScripts = collider2D.gameObject.GetComponent<AirBaseScript>();
                airBaseScripts.AircraftLanding(this, aircraftData);
            }
        }

        public void BaseInRangeStateToBaseMove(Collider2D collider2D)
        {
            AirBaseScript airBaseScripts = collider2D.gameObject.GetComponent<AirBaseScript>();
            (pathHandlerBase as PathHandler).MakeLandingPath(transform.position, airBaseScripts.GetLandingPoint(),airBaseScripts.gameObject.transform);
            state = State.Landing;
        }
        private void TouchPathMaking()
        {
            if (Input.touchCount > 0)
            {
                TouchPathMakingOnTouch();
            }
            else
            {
                gameManager.isPathMaking = false;
                pathMakingState = PathMakingState.None;
            }
        }

        private void TouchPathMakingOnTouch()
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
        

    }

    public enum State
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