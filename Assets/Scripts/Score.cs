using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public static int moneyAmount = 0;
    private Text moneyText;


    void Start()
    {
        moneyText = GetComponent<Text>();
        moneyAmount = 0;
    }


    void Update()
    {
        moneyText.text = "$: " + moneyAmount;    
    }
}
