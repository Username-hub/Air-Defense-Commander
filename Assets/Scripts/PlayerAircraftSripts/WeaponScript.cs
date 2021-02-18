using System;
using DefaultNamespace.EnemyScripts;
using UnityEngine;

namespace DefaultNamespace.PlayerAircraftSripts
{
    public class WeaponScript : MonoBehaviour
    {
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

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                enemyInRanage(other.gameObject.GetComponent<EnemyAircraftScript>());  
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