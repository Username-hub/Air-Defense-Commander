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
        public float maxFuel;
        public float fuelConsumptionRate;
        private float currentFuel;
        public PlayerPathHandler pathHandlerBase;
        public GameManager gameManager;
        public UnitInfoScript unitInfoScript;
        public PlayerAircraftMoveHandler aircraftMoveHandler;
        public PathMakingHandler pathMakingHandler;
        
        public AircraftData aircraftData;
        
        private void Start()
        {
            currentFuel = maxFuel;
            pathHandlerBase = GetComponent<PlayerPathHandler>();
            pathMakingHandler = new PathMakingHandler(gameManager,pathHandlerBase,aircraftMoveHandler,this);
        }

        private void Update()
        {
            if (gameManager.gameState != GameManager.GameState.paused)
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
            currentFuel -= fuelConsumptionRate * Time.deltaTime;
                
            UpdateAircrafData();
            UpdateAircrafUI();
        }

        public void UpdateAircrafData()
        {
            aircraftData.HP = currentHealth;
            aircraftData.fuel = currentFuel;
        }

        protected void UpdateAircrafUI()
        {
            unitInfoScript.SetRotationOffset(transform.eulerAngles.z);
            unitInfoScript.UpdateBars(currentHealth,maxHealth,maxFuel,currentFuel);
        }

    }
    
    
    
}