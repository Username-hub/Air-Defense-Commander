using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class AnimatorScroipt : MonoBehaviour
{
    public AircraftScript aircraftScript;

    public void DeathAnimationEnd()
    {
        aircraftScript.DeathAnimationEnd();
    }
}
