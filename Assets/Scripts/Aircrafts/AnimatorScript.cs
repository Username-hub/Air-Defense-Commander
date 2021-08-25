using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.EnemyScripts;
using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    public EnemyAircraftScript enemyAircraftScript;

    public void DeathAnimationEnd()
    {
        enemyAircraftScript.DeathAnimationEnd();
    }
}
