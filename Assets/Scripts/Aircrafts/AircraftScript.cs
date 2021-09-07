using System;
using DefaultNamespace.EnemyScripts;
using UnityEngine;

namespace DefaultNamespace
{
    public class AircraftScript : MonoBehaviour
    {
        public float toAircraftDamage;
        
        public float maxHealth;
        protected float currentHealth;
        public float maxFuel;
        public float fuelConsumptionRate;
        protected float currentFuel;
        public PathHandlerBase pathHandlerBase;
        public GameManager gameManager;
        public UnitInfoScript unitInfoScript;
        public AircraftMoveHandler aircraftMoveHandler;
        public float GetCurrentHealth()
        {
            return currentHealth;
        }
        protected void UpdateAircrafUI()
        {
            //unitInfoScript.UpdateBars(currentHealth,maxHealth,maxFuel,currentFuel);
        }

        public void DeathAnimationEnd()
        {
            Destroy(gameObject);
        }
        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
        }

        
        
        private void Start()
        {
            throw new NotImplementedException();
        }
    }
    
}