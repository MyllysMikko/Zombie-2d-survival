using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public static int moneyAmount = 0;
    private TextMeshProUGUI moneyText;


    void Start()
    {
        moneyText = GetComponent<TextMeshProUGUI>();
        moneyAmount = 0;
    }


    void Update()
    {
        moneyText.text = "$: " + moneyAmount;    
    }
}
