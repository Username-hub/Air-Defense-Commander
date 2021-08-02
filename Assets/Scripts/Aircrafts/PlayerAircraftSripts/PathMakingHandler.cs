using UnityEngine;

namespace DefaultNamespace.PlayerAircraftSripts
{
    public class PathMakingHandler
    {
        private GameManager gameManager;
        private PathMakingState pathMakingState;
        private PlayerPathHandler pathHandlerBase;
        private PlayerAircraftMoveHandler aircraftMoveHandler;
        private PlayerAircraftScript playerAircraftScript;

        public PathMakingHandler(GameManager gameManager, PlayerPathHandler pathHandlerBase, 
            PlayerAircraftMoveHandler aircraftMoveHandler, PlayerAircraftScript playerAircraftScript)
        {
            pathMakingState = PathMakingState.None;
            this.gameManager = gameManager;
            this.pathHandlerBase = pathHandlerBase;
            this.aircraftMoveHandler = aircraftMoveHandler;
            this.playerAircraftScript = playerAircraftScript;
        }

        public void OnUpdatePathMaker()
        {
            if (pathMakingState == PathMakingState.MakingPath)
            {
                TouchPathMaking();
            }
        }
        private void TouchPathMaking()
        {
            if (Input.touchCount > 0)
            {
                PathMakingOnTouch();
            }
            else
            {
                gameManager.isPathMaking = false;
                pathMakingState = PathMakingState.None;
            }
        }

        private void PathMakingOnTouch()
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
                    pathHandlerBase.ToBasePath(touchPos);
                    aircraftMoveHandler.SetState(PlayerAircraftMoveHandler.State.ToBaseMove);
                }

                if (collider2D.tag == "Enemy")
                {
                    pathHandlerBase.ChaseEnemyPath(collider2D.gameObject);
                    aircraftMoveHandler.SetState(PlayerAircraftMoveHandler.State.FollowAircraft);
                }
            }
            else
            {
                pathHandlerBase.AddPointToPath(touchPos);
            }
        }
        
        public void StartPathMaking()
        {
            pathHandlerBase.CreateNewPath(playerAircraftScript.transform.position);
            pathMakingState = PathMakingState.MakingPath;
            aircraftMoveHandler.SetState(PlayerAircraftMoveHandler.State.FollowPath);
        }
        
        public void StopPathMaking()
        {
            pathMakingState = PathMakingState.None;
            gameManager.isPathMaking = false;
        }
        
        enum PathMakingState
        {
            MakingPath,
            None
        }
    }
}