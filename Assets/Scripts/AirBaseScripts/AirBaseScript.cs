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

        public void SpawnBaseUI(GameObject mainCameraCanvas)
        {
            BaseUI = Instantiate(BaseUIPrefab,
                new Vector3(Input.touches[0].position.x, Input.touches[0].position.y,
                    mainCameraCanvas.transform.position.z), Quaternion.identity, mainCameraCanvas.transform);
            BaseUI.GetComponent<AircraftChosePanel>().airBaseScript = this;

        }

        private void Update()
        {
            if (BaseUI != null)
            {
                if (Input.touchCount == 0)
                {
                    Destroy(BaseUI);
                }
            }
        }

        public void FighterButton()
        {
            runwayAnimator.Play("StartAnimation");
        }

        public void StartAnimationEnd()
        {
            Instantiate(FighterPrefab, takeOff.transform.position, takeOff.transform.rotation);
        }
        
    }
}