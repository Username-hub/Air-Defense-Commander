using System;
using DefaultNamespace.AirBaseScripts;
using DefaultNamespace.EnemyScripts;
using UnityEngine;

namespace DefaultNamespace.PlayerAircraftSripts
{
    public class PlayerAircraftMoveHandler : MonoBehaviour
    {
        public float rotationSpeedForward;
        public float rotationAngleChaneSpeed;
        public float rotationTime;
        public float speed;
        public float maxSpeed;
        public float angle = 0;
        public PlayerAircraftScript playerAircraftScript;
        private PlayerPathHandler pathHandlerBase;
        private Rigidbody2D rigidbody2D;
        private State state;

        public State getState()
        {
            return state;
        }
        
        private void Start()
        {
            state = State.Wait;
            rigidbody2D = GetComponent<Rigidbody2D>();
            pathHandlerBase = playerAircraftScript.pathHandlerBase;
            speed = maxSpeed;
        }
        public void OnUpdateMove()
        {
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
                MoveForward(pathHandlerBase.getNextPoint());
            }

        }
        
        private void StateFollowPassUpdate()
        {
            if (pathHandlerBase.GetPathLength() == 0)
            {
                state = State.Wait;
            }
            else
            {
                MoveForward(pathHandlerBase.getNextPoint());
            }
        }
        
        private void StateWaitUpdate()
        {
            if (pathHandlerBase.GetPathLength() > 0)
                state = State.FollowPath;
            MoveInCircle();
        }
        
        private void StateFollowAircraftUpdate()
        {
            MoveForward(pathHandlerBase.GetEnemyToChasePos());
        }
        
        private void StateCloseFollowUpdate()
        {
            if ((pathHandlerBase).enemyToChase != null)
                MoveForward(pathHandlerBase.GetEnemyToChasePos());
            else
            {
                state = State.FollowPath;
                speed = maxSpeed;
            }
        }
        
        public void GoToWaitState()
        {
            pathHandlerBase.CleatPath();
            state = State.Wait;
        }
        
        public void BaseInRange(Collider2D collider2D)
        {
            if (state == State.ToBaseMove)
            {
                BaseInRangeStateToBaseMove(collider2D);   
            }else if (state == State.Landing & pathHandlerBase.GetPathLength() < 2)
            {
                AirBaseScript airBaseScripts = collider2D.gameObject.GetComponent<AirBaseScript>();
                airBaseScripts.AircraftLanding(playerAircraftScript, playerAircraftScript.aircraftData);
            }
        }
        
        public void EnemyInSootRange(GameObject enemyGameObjet)
        {
            if (state == State.FollowAircraft)
            {
                if (enemyGameObjet == pathHandlerBase.enemyToChase)
                {
                    EnemyAircraftScript enemyAircraftScript =
                        pathHandlerBase.enemyToChase.GetComponent<EnemyAircraftScript>();
                    speed = enemyAircraftScript.GetAircraftSpeed();
                    pathHandlerBase.enemyToChase = enemyAircraftScript.enemyTail;
                    state = State.CloseFollow;
                }
            }
        }

        public void BaseInRangeStateToBaseMove(Collider2D collider2D)
        {
            AirBaseScript airBaseScripts = collider2D.gameObject.GetComponent<AirBaseScript>();
            pathHandlerBase.MakeLandingPath(transform.position, airBaseScripts.GetLandingPoint(),airBaseScripts.gameObject.transform);
            state = State.Landing;
        }
        public bool MoveForward(Vector2 pointToMove)
        {
            Vector2 position = transform.position;
            if (pointToMove != position)
            {
                float ang = Mathf.Atan2((pointToMove.y - position.y) , (pointToMove.x - position.x));
                transform.eulerAngles = new Vector3(0, 0, ang * Mathf.Rad2Deg);
                
                position = Vector2.MoveTowards(position, pointToMove, (speed * Time.deltaTime));

                angle = ang;
                gameObject.transform.localPosition = position;
                return CheckPointReach();
            }

            return false;
        }
        
        public void MoveInCircle()
        {
            Vector3 rotation = transform.eulerAngles;
            rotation.z += rotationAngleChaneSpeed * Time.deltaTime;
            transform.eulerAngles = rotation;
            transform.localPosition += transform.right * (rotationSpeedForward * Time.deltaTime);
        }
        
        protected bool CheckPointReach()
        {
            if(state == State.FollowPath || state == State.Landing)
            {
                pathHandlerBase.PointReached();
                return true;
            }
            return false;
        }

        public void SetState(State setState)
        {
            state = setState;
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
    }
}