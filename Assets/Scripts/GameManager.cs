using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.AirBaseScripts;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public GameObject mainUiCanvas;
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchColider = TouchControlHandler.getOnTouchCollider(touch);
                if (touchColider != null)
                {
                    if (touchColider.tag == "aircraft")
                    {
                        touchColider.gameObject.GetComponent<AircraftScript>().StartPathMaking();
                    }else if (touchColider.tag == "Base")
                    {
                        touchColider.gameObject.GetComponent<AirBaseScript>().SpawnBaseUI(mainUiCanvas);
                    }
                }
                
            }
            if (touch.phase == TouchPhase.Moved)
            {
            }
        }
    }

    
    
}