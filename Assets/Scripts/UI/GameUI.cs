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
    private Image shovelImage;

    [SerializeField]
    private TextMeshProUGUI scoreText;

    private Animator animator;
    private float defaultHPImageWidth;

    private int hpValue;
    private int scoreValue;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        defaultHPImageWidth = playerHPImage.rectTransform.sizeDelta.x;
    }

    public void UpdateHPText(int value, bool valueIncreased, bool skipAnimation = false)
    {
        hpValue = value;

        if (skipAnimation) HPAnimationEnded();
        else if (valueIncreased) animator.SetTrigger("HPIncreased");
        else animator.SetTrigger("HPDecreased");
    }

    public void UpdateScoreText(int value, bool valueIncreased, bool skipAnimation = false)
    {
        scoreValue = value;

        if (skipAnimation) ScoreAnimationEnded();
        else if (valueIncreased) animator.SetTrigger("ScoreIncreased");
        else animator.SetTrigger("ScoreDecreased");
    }

    public void SetActiveShovel(bool value)
    {
        shovelImage.enabled = value;
    }

    private void HPAnimationEnded()
    {
        playerHPImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, defaultHPImageWidth * hpValue);
    }

    private void ScoreAnimationEnded()
    {
        scoreText.text = $"SCORE: {scoreValue}";
    }
}
