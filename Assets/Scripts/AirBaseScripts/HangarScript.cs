using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.AirBaseScripts
{
    public class HangarScript : MonoBehaviour
    {
        public float hpRecoverySpeed;

        private List<AircraftData> aircraftsInHangar;
        private List<AircraftData> aircraftsOutOfHangar;


        private void Update()
        {
            AircraftsRestoration();
        }

        private void AircraftsRestoration()
        {
            foreach (var aircraft in aircraftsInHangar)
            {
                aircraft.RecoverAircraft(hpRecoverySpeed);
            }
        }

        public void SetupAircraftList(List<AircraftData> aircraftDatas)
        {
            aircraftsInHangar = new List<AircraftData>(aircraftDatas);
            aircraftsOutOfHangar = new List<AircraftData>();
        }

        public List<AircraftData> GetAircraftsInHangar()
        {
            return aircraftsInHangar;
        }
        
        public List<AircraftData> GetAircraftsOutOfHangar()
        {
            return aircraftsOutOfHangar;
        }

        public void AircrcaftTakesOf(AircraftData takeOfAircraft)
        {
            aircraftsOutOfHangar.Add(takeOfAircraft);
            aircraftsInHangar.RemoveAt(aircraftsInHangar.IndexOf(takeOfAircraft));
        }

        public void AircraftLands(AircraftData landedAircraft)
        {
            aircraftsInHangar.Add(landedAircraft);
            aircraftsOutOfHangar.RemoveAt(aircraftsOutOfHangar.IndexOf(landedAircraft));
        }
    }
}