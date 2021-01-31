using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace.AirBaseScripts
{
    public class AircraftChoseButton : MonoBehaviour
    {
        public Collider2D collider2D;
        public int buttonNumber;
        public AircraftChosePanel aircraftChosePanel;
        public void ButtonPressed()
        {
            aircraftChosePanel.ButtonPressed(buttonNumber);
        }

        private void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject(0))
            {
                ButtonPressed();
            }
        }
    }
}