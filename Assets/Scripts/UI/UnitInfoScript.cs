using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfoScript : MonoBehaviour
{
    public Canvas unitInfoCanvas;

    public Image healthBar;
    public Image fuelBar;
    private RectTransform rectTransform;

    public GameObject playerAircraft;
    // Start is called before the first frame update
    void Start()
    {
        unitInfoCanvas = GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        SetRotationOffset();
    }

    private void Update()
    {
        SetRotationOffset();
    }

    public void SetRotationOffset()
    {
        rectTransform.localRotation = Quaternion.Euler(new Vector3(0,0,-playerAircraft.transform.eulerAngles.z));
    }

    public void UpdateBars(float currentHealth, float maxHealth,float fuelFillAmount)
    {
        healthBar.fillAmount = currentHealth / maxHealth;
        fuelBar.fillAmount = fuelFillAmount;
    }
}
