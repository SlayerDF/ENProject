using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingUI : MonoBehaviour
{
    [SerializeField]
    private Button restartLevelButton;

    [SerializeField]
    private Button loadMainMenuButton;

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
    private void Awake()
    {
        restartLevelButton.onClick.AddListener(RestartLevel);
        loadMainMenuButton.onClick.AddListener(LoadMainMenu);
    }

    private void OnDestroy()
    {
        restartLevelButton.onClick.RemoveListener(RestartLevel);
        loadMainMenuButton.onClick.RemoveListener(LoadMainMenu);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

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
