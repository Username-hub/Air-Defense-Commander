using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.AirBaseScripts
{
    public class TemporaryHangarFill : MonoBehaviour
    {
        public Sprite aircraftSprite;
        public AircraftType aircraftType;
        private float maxHP;
        public float HP;
        public HangarScript hangarScript;

        private void Start()
        {
            List<AircraftData> AD = new List<AircraftData>();
            AD.Add(new AircraftData(aircraftSprite,aircraftType,maxHP,HP));
            AD.Add(new AircraftData(aircraftSprite,aircraftType,maxHP,HP));
            AD.Add(new AircraftData(aircraftSprite,aircraftType,maxHP,HP));
            AD.Add(new AircraftData(aircraftSprite,aircraftType,maxHP,HP));
            
            hangarScript.SetupAircraftList(AD);
        }
    }
}