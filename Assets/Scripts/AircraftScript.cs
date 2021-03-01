using System;
using DefaultNamespace.EnemyScripts;
using UnityEngine;

namespace DefaultNamespace
{
    public class AircraftScript : MonoBehaviour
    {
        public int toAircraftDamage;

        public float speed;
        public int maxHealth;
        protected int currentHealth;
        public PathHandlerBase pathHandlerBase;
        public GameManager gameManager;
        public UnitInfoScript unitInfoScript;

        protected void UpdateAircrafUI()
        {
            unitInfoScript.SetRotationOffset(transform.eulerAngles.z);
        }

        public void DeathAnimationEnd()
        {
            Destroy(gameObject);
        }
        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
        }
        private void Start()
        {
            throw new NotImplementedException();
        }
        
        protected float angle = 0;

        protected void MoveForward(Vector2 toMovePoint)
        {
            Vector2 position = transform.position;
            if (toMovePoint != position)
            {
                float ang = Mathf.Atan2((toMovePoint.y - position.y) , (toMovePoint.x - position.x));
                transform.eulerAngles = new Vector3(0, 0, ang * Mathf.Rad2Deg);
                
                position = Vector2.MoveTowards(position, toMovePoint, (speed * Time.deltaTime));

                angle = ang;
                gameObject.transform.localPosition = position;
                CheckPointReach();
            }
        }

        protected virtual void CheckPointReach()
        {
            pathHandlerBase.PointReached();
        }
        
    }
    
}