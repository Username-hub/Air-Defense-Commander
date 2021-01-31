using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.AirBaseScripts;
using UnityEngine;

public class TakeOffScript : MonoBehaviour
{
    public AirBaseScript airBaseScript;

    // Update is called once per frame
    public void StartAnimationEnd()
    {
        airBaseScript.StartAnimationEnd();
    }
}
