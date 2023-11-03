using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHPController : MonoBehaviour
{
    [SerializeField] int maxHP;
    [SerializeField] int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }



    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            //TODO DIE
        }
    }
}
