using UnityEngine;

namespace DefaultNamespace.PlayerAircraftSripts
{
    public class PlayerFighterAnimatorScript : MonoBehaviour
    {
        [SerializeField]
        private Animator playerAircraftAnimator;

        [SerializeField]
        private PlayerAircraftScript playerAircraftScript;
        public void StartDeathAnimation()
        {
            playerAircraftAnimator.Play("DeathAnimation");
        }

        public void EndDeathAnimation()
        {
            playerAircraftScript.DeathAnimationEnd();
        }
    }
}