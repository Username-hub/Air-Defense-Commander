using System;
using System.Collections.Generic;
using DefaultNamespace.EnemyScripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class EnemySpawnerScript : MonoBehaviour
    {
        [SerializeField]
        public List<EnemySpawnInformation> enemySpawnInformations;
        public List<EnemySpawnInformation> enemySpawnInformationsLocal;
        public Transform[] spawnLocation;
        public GameManager gameManager;
        public GameObject enemyBomberPrefab;
        
        private float timerSpawn;
        private int enemyToKill;

        public void EnemyDestroy()
        {
            enemyToKill--;
            if (enemyToKill <= 0)
            {
                gameManager.LevelWin();
            }
        }
        private void Start()
        {
            timerSpawn = 0;
            enemyToKill = enemySpawnInformations.Count;
            enemySpawnInformationsLocal = new List<EnemySpawnInformation>(enemySpawnInformations);
        }

        private void Update()
        {
            timerSpawn += Time.deltaTime;
            foreach (var enemy in enemySpawnInformationsLocal)
            {
                if (enemy.spawnTimer <= timerSpawn)
                {
                    SpawnEnemy(enemy);
                    break;
                }
            }
        }

        private void SpawnEnemy(EnemySpawnInformation enemySpawnInformation)
        {
            switch (enemySpawnInformation.enemyAircraftType)
            {
                case EnemyAircraftType.Bomber :
                    SpawnEnemyBomber(enemySpawnInformation);
                    break;
            }
        }

        private void SpawnEnemyBomber(EnemySpawnInformation enemySpawnInformation)
        {
            Transform tf = GetSpawner(enemySpawnInformation.SpawnerNumber);
            GameObject enemyAircraftObject =
                Instantiate(enemyBomberPrefab, tf.position, Quaternion.identity);
            EnemyAircraftScript enemyAircraftScript = enemyAircraftObject.GetComponent<EnemyAircraftScript>();
            enemyAircraftScript.gameManager = gameManager;
            enemyAircraftScript.toAircraftDamage = enemySpawnInformation.toAircraftDamage;
            enemyAircraftScript.maxHealth = enemySpawnInformation.MaxHealth;
            enemyAircraftScript.bombDamage = enemySpawnInformation.bombDamage;
            enemyAircraftScript.enemyAim = enemySpawnInformation.enemyAim;
            enemyAircraftScript.enemySpawnerScript = this;
            enemyAircraftScript.SetAircraftSpeedInMoveHandler(enemySpawnInformation.Speed);
            enemySpawnInformationsLocal.RemoveAt(enemySpawnInformationsLocal.IndexOf(enemySpawnInformation));

        }

        private Transform GetSpawner(int spawnerNum)
        {
            if (spawnerNum >= 0 && spawnerNum < spawnLocation.Length)
            {
                return spawnLocation[spawnerNum];
            }
            else
            {
                return spawnLocation[Random.Range(0, spawnLocation.Length)];
            }
        }
    }
    
    
    

    
    public enum EnemyAircraftType
    {
        Bomber,
        Fighter
    }
}