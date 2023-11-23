using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;


public class abilityController : Weapon
{
    public bool isAbilityActive;
    public bool CanIUseAbility = true;
    [SerializeField] private float currentTime = 0f;
    private float startTime = 30f;
    [SerializeField] private float abilityDuration = 5;  

    [SerializeField]TextMeshProUGUI coolDownText;

    
    void Start()
    {
        currentTime = startTime;
        CanIUseAbility = true;
    }

    void Update()
    {
        

        if (Input.GetKey(KeyCode.G))
        {
            CanIUseAbility = false;
            isAbilityActive = true;
            CalculateDamage();
            
        }

        if (CanIUseAbility == false)
        {
            isAbilityActive = true;
            CalculateDamage();

            if (currentTime > 0)
            {
                currentTime -= 1 * Time.deltaTime;
                abilityDuration -= 1 * Time.deltaTime;
            }
            coolDownText.text = currentTime.ToString("0");


            if (currentTime <= 0)
            {
                isAbilityActive = false;

                
                CanIUseAbility = true;
                if (abilityDuration <= 0)
                {
                    Reset();
                    if (Input.GetKey(KeyCode.G))
                    {

                        CanIUseAbility = false;
                        isAbilityActive = true;


                    }
                    else
                    {
                        isAbilityActive = false;

                    }

                    isAbilityActive = false;

                    CanIUseAbility = true;
                }
                if (CanIUseAbility == false)
                {
                    isAbilityActive = true;

                    CanIUseAbility = false;
                    
                }
                else
                {
                    isAbilityActive = false;

                }
            }
        }




    }

    public override int CalculateDamage()
    {
        if (isAbilityActive == true)
        {
           
              
            return damage * 2;
        }

        else
        {
            return damage;
        }
            
    }

    private void Reset()
    {
        currentTime = 30f;
        abilityDuration = 5f;
    }

}
