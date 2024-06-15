using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private Image playerHPImage;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    private float defaultHPImageWidth;

    private void Awake()
    {
        defaultHPImageWidth = playerHPImage.rectTransform.sizeDelta.x;
    }

    public void UpdateHPText(int value)
    {
        playerHPImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, defaultHPImageWidth * value);
    }

    public void UpdateScoreText(int value)
    {
        scoreText.text = $"SCORE: {value}";
    }
}
