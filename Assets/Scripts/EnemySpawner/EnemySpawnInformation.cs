using UnityEngine;

namespace DefaultNamespace
{
    [System.Serializable]
    public class EnemySpawnInformation 
    {
        public float spawnTimer;
        public EnemyAircraftType enemyAircraftType;
        public int SpawnerNumber;
        public float toAircraftDamage;
        public float Speed;
        public float MaxHealth;
        public float bombDamage;
        public Transform enemyAim;
        
    }
}