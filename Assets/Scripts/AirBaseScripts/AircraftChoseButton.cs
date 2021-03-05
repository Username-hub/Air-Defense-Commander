using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

namespace DefaultNamespace.AirBaseScripts
{
    public class AircraftChoseButton : MonoBehaviour, IPointerEnterHandler
    {
        public Collider2D collider2D;
        public AircraftChosePanel aircraftChosePanel;
        public void ButtonPressed()
        {
            aircraftChosePanel.ButtonPressed(assighnAircraftData);
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (Input.touchCount > 0 & !outOfHangarPanel.activeSelf)
            {
                Touch touch = Input.GetTouch(0);
                if (collider2D.OverlapPoint(touch.position))
                {
                    ButtonPressed();
                }
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ButtonPressed();
        }
        public AircraftData assighnAircraftData;
        public Image aircraftSprite;
        public GameObject outOfHangarPanel;
        public void SetUpButton(AircraftData aircraftData, bool isInHangar)
        {
            assighnAircraftData = aircraftData;
            aircraftSprite.sprite = aircraftData.aircraftSprite;
            if(!isInHangar)
                outOfHangarPanel.SetActive(true);
        }
        
    }
}