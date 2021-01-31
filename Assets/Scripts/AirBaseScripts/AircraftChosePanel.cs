using UnityEngine;

namespace DefaultNamespace.AirBaseScripts
{
    public class AircraftChosePanel : MonoBehaviour
    {
        public AirBaseScript airBaseScript;
        public void ButtonPressed(int buttonNumber)
        {
            switch (buttonNumber)
            {
                case 0:
                    airBaseScript.FighterButton();
                    break;
            }
            Destroy(gameObject);
        }
    }
    
}