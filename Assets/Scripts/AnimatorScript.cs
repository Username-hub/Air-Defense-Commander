using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class AnimatorScript : MonoBehaviour
{
    public AircraftScript aircraftScript;

    public void DeathAnimationEnd()
    {
        aircraftScript.DeathAnimationEnd();
    }
}
