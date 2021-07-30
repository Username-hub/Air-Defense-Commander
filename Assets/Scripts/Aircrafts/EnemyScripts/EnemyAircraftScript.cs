using System;
using DefaultNamespace.Path;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace DefaultNamespace.EnemyScripts
{
    public class EnemyAircraftScript : AircraftScript
    {
        public float bombDamage;
        public Transform enemyAim;
        public GameObject enemyTail;
        public Animator enemyAnimator;
        public EnemySpawnerScript enemySpawnerScript;
        private void Start()
        {
            isReturning = false;
            currentHealth = maxHealth;
            pathHandlerBase = GetComponent<PathHandlerBase>();
            (pathHandlerBase as EnemyPathHandler).BuildPath(enemyAim.position);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Aim"))
            {
                BombTarget(other.gameObject);
            }
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
            (pathHandlerBase as EnemyPathHandler).BuildAfterBombingPath(returnAim);
            isReturning = true;
        }
        
        private bool isReturning;
        private void Update()
        {
            if (pathHandlerBase.GetPositionsCount() > 0)
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
                    aircraftMoveHandler.MoveForward(pathHandlerBase.getNextPoint());
                    UIUpdate();
                }
            }

        }

        private void UIUpdate()
        {
            unitInfoScript.SetRotationOffset(transform.eulerAngles.z);
            unitInfoScript.UpdateBars(currentHealth,maxHealth,0,1);
            if (currentHealth <= 0)
            {
                pathHandlerBase.CleatPath();
                enemyAnimator.Play("EnemyBomberDeathAnimation");
            }
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