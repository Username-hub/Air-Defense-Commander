using UnityEngine;

namespace DefaultNamespace.EnemyScripts
{
    public class EnemyBomberAnimatorScript : MonoBehaviour
    {
        public EnemyAircraftScript enemyAircraftScript;
        public Animator animator;
        
        private AimFactoryScript aimFactoryScript;
        public void StartBombAnimation(AimFactoryScript aimFactoryScript)
        {
            this.aimFactoryScript = aimFactoryScript;
            animator.Play("BombingAnimation");
            enemyAircraftScript.ReturnToBase();
        }

        public void DamageAim()
        {
            enemyAircraftScript.DropBomb(aimFactoryScript);
        }

        public void AnimationEnd()
        {
            
        }

    }
}