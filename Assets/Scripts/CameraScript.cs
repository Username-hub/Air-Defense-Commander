using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class CameraScript : MonoBehaviour
    {
        public Camera mainCamera;
        public GameManager gameManager;
        public float panSpeed;
        public float zoomSpeed;
        
        private float[] boundsX = new float[]{-15,15};
        private float[] boundsY = new float[]{-15,15};
        private float[] zoomBounds = new float[] {5, 20};

        private void Update()
        {
            if(!gameManager.isPathMaking && gameManager.gameState != GameManager.GameState.paused)
                HandleTouch();
        }

        private Vector3 lastPanPosition;
        private int panFingerId;
        private bool wasZoomingLastFrame;
        private Vector2[] lastZoomPositions;

        private void HandleTouch()
        {
            switch (Input.touchCount)
            {
                case 1://Panning
                    wasZoomingLastFrame = false;

                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        lastPanPosition = touch.position;
                        panFingerId = touch.fingerId;
                    }else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved)
                    {
                        PanCamera(touch.position);
                    }
                    break;
                
                case 2://Zooming
                    Vector2[] newPosition = new Vector2[]{Input.GetTouch(0).position, Input.GetTouch(1).position};
                    if (!wasZoomingLastFrame)
                    {
                        lastZoomPositions = newPosition;
                        wasZoomingLastFrame = true;
                    }
                    else
                    {
                        float newDistance = Vector2.Distance(newPosition[0], newPosition[1]);
                        float oldDistance = Vector2.Distance(lastZoomPositions[0], lastZoomPositions[1]);
                        float offset = newDistance - oldDistance;
                        
                        ZoomCamera(offset, zoomSpeed);

                        lastZoomPositions = newPosition;
                    }
                    break;
                
                default:
                    wasZoomingLastFrame = false;
                    break;
            }
        }

        private void PanCamera(Vector3 newPanPosition)
        {
            Vector3 offset = mainCamera.ScreenToViewportPoint(lastPanPosition - newPanPosition);
            Vector3 move = new Vector3(offset.x * panSpeed, offset.y * panSpeed, 0);
            
            transform.Translate(move, Space.World);

            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(transform.position.x, boundsX[0], boundsX[1]);
            pos.y = Mathf.Clamp(transform.position.y, boundsY[0], boundsY[1]);
            transform.position = pos;

            lastPanPosition = newPanPosition;
        }

        private void ZoomCamera(float offset, float speed)
        {
            if (offset == 0)
            {
                return;
            }

            mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize - (offset * speed), zoomBounds[0],
                zoomBounds[1]);
        }
    }
}