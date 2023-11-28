using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;


public class abilityController : MonoBehaviour
{
    public bool isAbilityActive;
    public bool CanIUseAbility = true;
    [SerializeField] private float AbilityCoolDownTime = 0f;
    private float startTime = 15f;
    [SerializeField] private float abilityDuration = 5f;  

    [SerializeField]TextMeshProUGUI coolDownText;

    
    void Start()
    {
        AbilityCoolDownTime = startTime;
        CanIUseAbility = true;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.G) && !isAbilityActive)
        {
            StartCoroutine(Ability());
        }



        if(CanIUseAbility == false)
        {
            if(AbilityCoolDownTime > 0)
            {
                AbilityCoolDownTime -= 1 * Time.deltaTime;
                abilityDuration -= 1 * Time.deltaTime;

            }

            if(abilityDuration <= 0)
            {
                isAbilityActive = false;

                if (AbilityCoolDownTime <= 0)
                {
                    AbilityCoolDownTime = startTime;
                    abilityDuration = 5f;
                    
                }
            }


        }    


        //if (Input.GetKey(KeyCode.G))
        //{
        //    CanIUseAbility = false;
        //    isAbilityActive = true;


        //}

        //if (CanIUseAbility == false)
        //{
        //    isAbilityActive = true;


        //    if (currentTime > 0)
        //    {
        //        currentTime -= 1 * Time.deltaTime;
        //        abilityDuration -= 1 * Time.deltaTime;
        //    }
        //    coolDownText.text = currentTime.ToString("0");


        //    if (currentTime <= 0)
        //    {
        //        isAbilityActive = false;
        //        CanIUseAbility = true;


        //        if (abilityDuration <= 0)
        //        {
        //            Reset();
        //            if (Input.GetKey(KeyCode.G))
        //            {

        //                CanIUseAbility = false;
        //                isAbilityActive = true;


        //            }
        //            else
        //            {
        //                isAbilityActive = false;

        //            }

        //            isAbilityActive = false;

        //            CanIUseAbility = true;
        //        }
        //        if (CanIUseAbility == false)
        //        {
        //            isAbilityActive = true;

        //            CanIUseAbility = false;

        //        }
        //        else
        //        {
        //            isAbilityActive = false;

        //        }
        //    }
        //}




    }

    IEnumerator Ability()
    {
        CanIUseAbility = false;
        isAbilityActive = true;
        yield return new WaitForSeconds(abilityDuration);
        isAbilityActive = false;
        yield return new WaitForSeconds(AbilityCoolDownTime);
        CanIUseAbility = true;
    }


}
