using System;
using UnityEngine;

namespace DefaultNamespace.PlayerAircraftSripts
{
    public class PlayerAircraftFuel : MonoBehaviour
    {
        [SerializeField]
        private float maxFuel;
        public float MaxFuel
        {
            get => maxFuel;
        }

        [SerializeField]
        private float currentFuel;
        
        public float CurrentFuel
        {
            get => currentFuel;
        }

        [SerializeField]
        private float fuelUsage;

        private float fuelFillAmount;

        public float FuelFillAmount
        {
            get => fuelFillAmount;
        }

        private void Start()
        {
            currentFuel = maxFuel;
            fuelFillAmount = currentFuel / maxFuel;
        }

        private void Update()
        {
            if (currentFuel > 0)
            {
                currentFuel -= fuelUsage * Time.deltaTime;
            }
            else
            {
                FuelIsOut();
            }
            SetFillAmount();
        }

        private void SetFillAmount()
        {
            fuelFillAmount = currentFuel / maxFuel;
        }

        [SerializeField]
        private PlayerAircraftScript playerAircraftScript;

        [SerializeField]
        private float fuelOutDamage;
        private void FuelIsOut()
        {
            playerAircraftScript.DamageAircraft(fuelOutDamage * Time.deltaTime);
        }
    }
}