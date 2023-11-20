using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public static int moneyAmount = 0;

    [SerializeField] int score;

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
        moneyText.text = "$: " + moneyAmount;    
    }

    void UpdateScore()
    {
        scoreText.text = $"Score: {score}";
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScore();
    }
}
