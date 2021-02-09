using System;
using System.Security.Cryptography;
using UnityEngine;

namespace DefaultNamespace.AirBaseScripts
{
    public class AirBaseScript : MonoBehaviour
    {
        public Animator runwayAnimator;
        public GameObject takeOff;
        public GameObject BaseUIPrefab;
        private GameObject BaseUI;
        public GameObject FighterPrefab;
        public GameManager gameManager;
        public void SpawnBaseUI(GameObject mainCameraCanvas)
        {
            BaseUI = Instantiate(BaseUIPrefab,
                new Vector3(Input.touches[0].position.x, Input.touches[0].position.y,
                    mainCameraCanvas.transform.position.z), Quaternion.identity, mainCameraCanvas.transform);
            BaseUI.GetComponent<AircraftChosePanel>().airBaseScript = this;
            gameManager.isPathMaking = true;

        }

        private void Update()
        {
            if (BaseUI != null)
            {
                if (Input.touchCount == 0)
                {
                    Destroy(BaseUI);
                    gameManager.isPathMaking = false;
                }
            }
        }

        public void FighterButton()
        {
            runwayAnimator.Play("StartAnimation");
            gameManager.isPathMaking = false;
        }

        public void StartAnimationEnd()
        {
            GameObject fighter =Instantiate(FighterPrefab, takeOff.transform.position, takeOff.transform.rotation);
            fighter.GetComponent<AircraftScript>().gameManager = gameManager;

        }
        
    }
}