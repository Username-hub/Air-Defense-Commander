using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.AirBaseScripts
{
    public class AircraftChosePanel : MonoBehaviour
    {
        public AirBaseScript airBaseScript;
        public void ButtonPressed(AircraftData aircraftData)
        {
            switch (aircraftData.aircraftType)
            {
                case AircraftType.Fighter:
                    airBaseScript.FighterButton(aircraftData);
                    break;
            }
            Destroy(gameObject);
        }


        public AircraftChoseButton[] buttons;
        public void ChosePanelSetUp(List<AircraftData> aircraftsInHangar, List<AircraftData> aircraftsOutOfHangar)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i < aircraftsInHangar.Count)
                {
                    buttons[i].SetUpButton(aircraftsInHangar[i],true);
                }
                else
                {
                    buttons[i].SetUpButton(aircraftsOutOfHangar[i - aircraftsInHangar.Count],false);
                }
            }
        }
    }
    
}