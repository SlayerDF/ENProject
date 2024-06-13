using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI playerHPText;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    public void UpdateHPText(int value)
    {
        playerHPText.text = $"LIVES: {value}";
    }

    public void UpdateScoreText(int value)
    {
        scoreText.text = $"SCORE: {value}";
    }
}
