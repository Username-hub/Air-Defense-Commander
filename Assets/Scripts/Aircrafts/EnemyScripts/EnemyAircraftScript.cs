using System;
using DefaultNamespace.Path;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace DefaultNamespace.EnemyScripts
{
    public class EnemyAircraftScript : MonoBehaviour
    {
        public float maxHealth;
        protected float currentHealth;
        public float bombDamage;
        public Transform enemyAim;
        public GameObject enemyTail;
        public Animator enemyAnimator;
        public EnemySpawnerScript enemySpawnerScript;
        public EnemyPathHandler enemyPathHandler;
        public EnemyUnitInfoScript enemyUnitInfoScript;
        public AircraftMoveHandler aircraftMoveHandler;
        public GameManager gameManager;
        
        private void Start()
        {
            isReturning = false;
            currentHealth = maxHealth;
            enemyPathHandler = GetComponent<EnemyPathHandler>();
            enemyPathHandler.BuildPath(enemyAim.position);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Aim"))
            {
                BombTarget(other.gameObject);
            }
        }

        public float GetAircraftSpeed()
        {
            return aircraftMoveHandler.speed;
        }
        
        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
        }
        
        public float GetCurrentHealth()
        {
            return currentHealth;
        }
        public EnemyBomberAnimatorScript enemyBomberAnimatorScript;
        public void BombTarget(GameObject target)
        {
            enemyBomberAnimatorScript.StartBombAnimation(target.GetComponent<AimFactoryScript>());
            
        }
        
        private Vector3 returnAim;
        public float distAfterBombing;

        public void DropBomb(AimFactoryScript aimFactoryScript)
        {
            aimFactoryScript.BombDropped(bombDamage);
        }

        public void ReturnToBase()
        {
            returnAim = new Vector3(transform.position.x + distAfterBombing * Mathf.Cos(transform.eulerAngles.z * Mathf.Deg2Rad),transform.position.y + distAfterBombing * Mathf.Sin(transform.eulerAngles.z * Mathf.Deg2Rad));
            enemyPathHandler.BuildAfterBombingPath(returnAim);
            isReturning = true;
        }
        
        private bool isReturning;
        private void Update()
        {
            if (enemyPathHandler.GetPositionsCount() > 0)
            {
                if (isReturning)
                {
                    if (aircraftMoveHandler.MoveForward(returnAim))
                    {
                        Destroy(gameObject);
                    }

                    UIUpdate();
                }
                else
                {
                    aircraftMoveHandler.MoveForward(enemyPathHandler.getNextPoint());
                    UIUpdate();
                }
            } 
        }

        private void UIUpdate()
        {
            enemyUnitInfoScript.SetRotationOffset(transform.eulerAngles.z);
            enemyUnitInfoScript.UpdateBars(currentHealth,maxHealth);
            //TODO: replace from UIUpdate
            if (currentHealth <= 0)
            {
                enemyPathHandler.CleatPath();
                enemyAnimator.Play("EnemyBomberDeathAnimation");
            }
        }
        
        public void SetAircraftSpeedInMoveHandler(float speed)
        {
            aircraftMoveHandler.speed = speed;
        }
        public void DeathAnimationEnd()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            enemySpawnerScript.EnemyDestroy();
        }
    }
} 