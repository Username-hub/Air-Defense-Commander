using DefaultNamespace.AirBaseScripts;
using DefaultNamespace.EnemyScripts;
using UnityEngine;

namespace DefaultNamespace.PlayerAircraftSripts
{
    public class PlayerAircraftScript : MonoBehaviour
    {

        public float toAircraftDamage;
        
        public float maxHealth;
        private float currentHealth;
        public PlayerPathHandler pathHandlerBase;
        public GameManager gameManager;
        public UnitInfoScript unitInfoScript;
        public PlayerAircraftMoveHandler aircraftMoveHandler;
        public PathMakingHandler pathMakingHandler;
        public AircraftData aircraftData;
        public PlayerAircraftFuel playerAircraftFuel;
        [SerializeField]
        private PlayerFighterAnimatorScript playerFighterAnimatorScript;
        
        
        private void Start()
        {
            currentHealth = maxHealth;
            pathHandlerBase = GetComponent<PlayerPathHandler>();
            pathMakingHandler = new PathMakingHandler(gameManager,pathHandlerBase,aircraftMoveHandler,this);
        }

        private void Update()
        {
            if (gameManager.gameState != GameManager.GameState.paused && currentHealth > 0)
            {
                OnGameNotPausedUpdate();
            }
                
        }

        private void OnGameNotPausedUpdate()
        {
            //Path making
            pathMakingHandler.OnUpdatePathMaker();
            //Move
            aircraftMoveHandler.OnUpdateMove();
                
            UpdateAircrafData();
            UpdateAircrafUI();
        }

        public void DamageAircraft(float damage)
        {
            currentHealth -= damage;
            UpdateAircrafUI();
            CheckIfDead();
        }

        private void CheckIfDead()
        {
            if (currentHealth <= 0)
            {
                OnDeath();
            }
        }

        private void OnDeath()
        {
            unitInfoScript.gameObject.SetActive(false);
            playerFighterAnimatorScript.StartDeathAnimation();
        }
        
        public void DeathAnimationEnd()
        {
            Destroy(gameObject);
        }
        public void UpdateAircrafData()
        {
            aircraftData.HP = currentHealth;
            aircraftData.fuel = playerAircraftFuel.CurrentFuel;
        }

        protected void UpdateAircrafUI()
        {
            unitInfoScript.UpdateBars(currentHealth,maxHealth, playerAircraftFuel.FuelFillAmount);
        }

    }
    
    
    
}