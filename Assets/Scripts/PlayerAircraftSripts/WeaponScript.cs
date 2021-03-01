using System;
using DefaultNamespace.EnemyScripts;
using UnityEngine;

namespace DefaultNamespace.PlayerAircraftSripts
{
    public class WeaponScript : MonoBehaviour
    {
        public float shootCallDawn;
        protected float currentShootCallDawn;
        public PlayerAircraftScript playerAircraftScript;
        public ParticleSystem leftGun;
        public ParticleSystem rightGun;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                playerAircraftScript.enemyInSootRange(other.gameObject);
            }
        }

        public void Start()
        {
            currentShootCallDawn = 0;
        }

        private void Update()
        {
            if (currentShootCallDawn > 0)
            {
                currentShootCallDawn -= Time.deltaTime;
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                if (currentShootCallDawn <= 0)
                {
                    enemyInRanage(other.gameObject.GetComponent<EnemyAircraftScript>());
                    EnemyAircraftScript enemyAircraftScript = other.gameObject.GetComponent<EnemyAircraftScript>();
                    enemyAircraftScript.TakeDamage(playerAircraftScript.toAircraftDamage);
                    currentShootCallDawn = shootCallDawn;
                }
            }
        }

        private void enemyInRanage(EnemyAircraftScript enemyAircraftScript)
        {
            leftGun.Play();
            rightGun.Play();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                leftGun.Stop();
                rightGun.Stop();
            }
        }
    }
}