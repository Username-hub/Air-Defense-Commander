using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfoScript : MonoBehaviour
{
    public Canvas unitInfoCanvas;

    public Image healthBar;

    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        unitInfoCanvas = GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetRotationOffset(float ang)
    {
        rectTransform.localRotation = Quaternion.Euler(new Vector3(0,0,-ang));
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
