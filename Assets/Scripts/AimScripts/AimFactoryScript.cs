using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimFactoryScript : MonoBehaviour
{
    public float maxHP;
    private float HP;

    private void Start()
    {
        HP = maxHP;
    }

    public void BombDropped(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        
    }
}
