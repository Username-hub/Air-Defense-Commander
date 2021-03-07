using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.AirBaseScripts;
using UnityEngine;

public class LandingScript : MonoBehaviour
{
    public AirBaseScript airBaseScript;

    public Animator landingAnimator;

    public void LandingAnimationStart()
    {
        landingAnimator.Play("LandingAnimation");
    }

    public void LandingAnimationEnd()
    {
        airBaseScript.LandingAnimationEnd();
    }
}
