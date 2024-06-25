using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingUI : MonoBehaviour
{
    [Header("UI elements")]

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

    [Header("Sounds")]

    [SerializeField]
    private AudioSource sfxAudioSource;

    [SerializeField]
    private AudioSource musicAudioSource;

    [SerializeField]
    private AudioClip buttonClickClip;

    [SerializeField]
    private AudioClip buttonHoverClip;

    [SerializeField]
    private AudioClip winMusicClip;

    [SerializeField]
    private AudioClip loseMusicClip;

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
        sfxAudioSource.Play(buttonClickClip);
    }

    private void PlayHoverSound()
    {
        if (!sfxAudioSource.isPlaying) sfxAudioSource.Play(buttonHoverClip);
    }

    // Scene switch methods

    private async void RestartLevel()
    {
        await sfxAudioSource.WaitFinish(ignoreTimeScale: true);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private async void LoadMainMenu()
    {
        await sfxAudioSource.WaitFinish(ignoreTimeScale: true);

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

        musicAudioSource.Play(winMusicClip);
    }

    public void ShowLoseScreen()
    {
        titleText.text = LoseText;
        subtitleText.enabled = false;

        ShowScreen();

        musicAudioSource.Play(loseMusicClip);
    }
}
