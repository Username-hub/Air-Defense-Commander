using System;
using DefaultNamespace.EnemyScripts;
using UnityEngine;

namespace DefaultNamespace
{
    public class AircraftScript : MonoBehaviour
    {
        public float toAircraftDamage;

        public float speed;
        public float maxHealth;
        protected float currentHealth;
        public PathHandlerBase pathHandlerBase;
        public GameManager gameManager;
        public UnitInfoScript unitInfoScript;

        public float GetCurrentHealth()
        {
            return currentHealth;
        }
        protected void UpdateAircrafUI()
        {
            unitInfoScript.SetRotationOffset(transform.eulerAngles.z);
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
        
        protected float angle = 0;

        protected bool MoveForward(Vector2 toMovePoint)
        {
            Vector2 position = transform.position;
            if (toMovePoint != position)
            {
                float ang = Mathf.Atan2((toMovePoint.y - position.y) , (toMovePoint.x - position.x));
                transform.eulerAngles = new Vector3(0, 0, ang * Mathf.Rad2Deg);
                
                position = Vector2.MoveTowards(position, toMovePoint, (speed * Time.deltaTime));

                angle = ang;
                gameObject.transform.localPosition = position;
                return CheckPointReach();
            }

            return false;
        }

        protected virtual bool CheckPointReach()
        {
            return pathHandlerBase.PointReached();
        }
        
    }
    
}