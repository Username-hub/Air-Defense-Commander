using System;
using DefaultNamespace.Path;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace DefaultNamespace.EnemyScripts
{
    public class EnemyAircraftScript : AircraftScript
    {
        public Transform enemyAim;
        public GameObject enemyTail;
        public Animator enemyAnimator;
        private void Start()
        {
            currentHealth = maxHealth;
            pathHandlerBase = GetComponent<PathHandlerBase>();
            (pathHandlerBase as EnemyPathHandler).BuildPath(enemyAim.position);
        }

        
        private void Update()
        {
            MoveForward(pathHandlerBase.getNextPoint());
            unitInfoScript.SetRotationOffset(transform.eulerAngles.z);
            unitInfoScript.UpdateBars(currentHealth,maxHealth);
            if (currentHealth <= 0)
            {
                enemyAnimator.Play("EnemyBomberDeathAnimation");
            }
                
        }

        public void DeathAnimationEnd()
        {
            Destroy(gameObject);
        }
    }
} 