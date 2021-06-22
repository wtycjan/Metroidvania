using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score=0;


    private void OnEnable()
    {
        Coin.OnCoinCollected += UpdateScore;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= UpdateScore;
    }

    private void UpdateScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }


}
