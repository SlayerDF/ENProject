using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingUI : MonoBehaviour
{
    [SerializeField]
    private HoverableButton restartLevelButton;

    [SerializeField]
    private HoverableButton loadMainMenuButton;

    [SerializeField]
    private string WinText;

    [SerializeField]
    private string LoseText;

    [SerializeField]
    private string pointsText;

    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private TextMeshProUGUI subtitleText;

    [SerializeField]
    private AudioManager audioManager;

    private Dictionary<HoverableButton, UnityAction> buttonActionMapping;

    private void Awake()
    {
        buttonActionMapping = new()
        {
            // Main menu events
            { restartLevelButton, RestartLevel },
            { loadMainMenuButton, LoadMainMenu }
        };
    }
    private void OnEnable()
    {
        foreach (var (button, action) in buttonActionMapping)
        {
            button.onClick.AddListener(PlayClickSound);
            button.onHover.AddListener(PlayHoverSound);
            button.onClick.AddListener(action);
        }
    }

    private void OnDisable()
    {
        foreach (var (button, action) in buttonActionMapping)
        {
            button.onClick.RemoveListener(action);
            button.onClick.RemoveListener(PlayClickSound);
            button.onHover.RemoveListener(PlayHoverSound);
        }
    }

    // Audio methods

    private void PlayClickSound()
    {
        audioManager.Play("Default", "ButtonClick");
    }

    private void PlayHoverSound()
    {
        audioManager.Play("Default", "ButtonHover", interrupt: false);
    }

    // Scene switch methods

    private async void RestartLevel()
    {
        await audioManager.WaitToFinishAll(ignoreTimeScale: true);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private async void LoadMainMenu()
    {
        await audioManager.WaitToFinishAll(ignoreTimeScale: true);

        SceneManager.LoadScene("MainMenu");
    }

    // State methods

    private void ShowScreen()
    {
        gameObject.SetActive(true);
    }

    public void ShowWinScreen(int points = 0)
    {
        titleText.text = WinText;
        subtitleText.text = $"{pointsText} {points}";

        ShowScreen();
    }

    public void ShowLoseScreen()
    {
        titleText.text = LoseText;
        subtitleText.enabled = false;

        ShowScreen();
    }
}
