using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public static int moneyAmount = 0;

    public int score { get; private set; }

    [SerializeField] TextMeshProUGUI moneyText;

    [SerializeField] TextMeshProUGUI scoreText;


    void Start()
    {
        moneyAmount = 0;

        score = 0;

        UpdateScore();
    }


    void Update()
    {
        moneyText.text = " " + moneyAmount;    
    }

    void UpdateScore()
    {
        scoreText.text = $"{score}";
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScore();
    }
}
