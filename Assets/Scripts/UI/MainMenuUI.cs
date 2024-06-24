using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Main menu
    [SerializeField]
    private GameObject mainMenuPanel;

    [SerializeField]
    private HoverableButton startGameButton;

    [SerializeField]
    private HoverableButton viewControlsButton;

    [SerializeField]
    private HoverableButton mainMenuButton;

    [SerializeField]
    private HoverableButton exitGameButton;

    // Levels menu
    [SerializeField]
    private GameObject levelsMenuPanel;

    [SerializeField]
    private HoverableButton levelTestButton;

    [SerializeField]
    private HoverableButton level1Button;

    [SerializeField]
    private HoverableButton levelsBackButton;

    // Controls menu
    [SerializeField]
    private GameObject controlsMenuPanel;

    [SerializeField]
    private GameObject[] controlsSlides;

    [SerializeField]
    private HoverableButton controlsPrevSlideButton;

    [SerializeField]
    private HoverableButton controlsNextSlideButton;

    [SerializeField]
    private HoverableButton controlsBackButton;

    // Other

    [SerializeField]
    private AudioManager audioManager;

    // Variables

    private Dictionary<HoverableButton, UnityAction> buttonActionMapping;

    private int slideIndex = 0;

    // State methods
    private void Awake()
    {
        buttonActionMapping = new()
        {
            // Main menu events
            { startGameButton, ShowLevelsMenu },
            { viewControlsButton, ShowControlsMenu },
            { mainMenuButton, LoadMainMenuScene },
            { exitGameButton, ExitGame },

            // Levels menu events
            { levelTestButton, LoadLevelTestScene },
            { level1Button, LoadLevel1Scene },
            { levelsBackButton, ShowMainMenu },

            // Controls menu events
            { controlsPrevSlideButton, PrevSlide },
            { controlsNextSlideButton, NextSlide },
            { controlsBackButton, ShowMainMenu }
        };
    }

    private void OnEnable()
    {
        slideIndex = 0;
        UpdateSlideButtonsVisual();
        ShowMainMenu();

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

    // Switch scene methods
    private async void LoadLevelTestScene()
    {
        await audioManager.WaitToFinishAll(ignoreTimeScale: true);

        SceneManager.LoadScene("LevelTest");
    }

    private async void LoadLevel1Scene()
    {
        await audioManager.WaitToFinishAll(ignoreTimeScale: true);

        SceneManager.LoadScene("Level1");
    }

    private async void LoadMainMenuScene()
    {
        await audioManager.WaitToFinishAll(ignoreTimeScale: true);

        SceneManager.LoadScene("MainMenu");
    }

    private async void ExitGame()
    {
        await audioManager.WaitToFinishAll(ignoreTimeScale: true);

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    // Switch menu methods
    private void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        levelsMenuPanel.SetActive(false);
        controlsMenuPanel.SetActive(false);
    }

    private void ShowLevelsMenu()
    {
        mainMenuPanel.SetActive(false);
        levelsMenuPanel.SetActive(true);
        controlsMenuPanel.SetActive(false);
    }

    private void ShowControlsMenu()
    {
        mainMenuPanel.SetActive(false);
        levelsMenuPanel.SetActive(false);
        controlsMenuPanel.SetActive(true);
    }

    // Switch slides methods
    private void PrevSlide()
    {
        if (IsFirstSlide()) return;

        controlsSlides[slideIndex].SetActive(false);

        slideIndex--;

        controlsSlides[slideIndex].SetActive(true);

        UpdateSlideButtonsVisual();
    }

    private void NextSlide()
    {
        if (IsLastSlide()) return;

        controlsSlides[slideIndex].SetActive(false);

        slideIndex++;

        controlsSlides[slideIndex].SetActive(true);

        UpdateSlideButtonsVisual();
    }

    private void UpdateSlideButtonsVisual()
    {
        EventSystem.current.SetSelectedGameObject(null);

        if (IsFirstSlide())
        {
            controlsPrevSlideButton.gameObject.SetActive(false);
        }
        else if (IsLastSlide())
        {
            controlsNextSlideButton.gameObject.SetActive(false);
        }
        else
        {
            controlsPrevSlideButton.gameObject.SetActive(true);
            controlsNextSlideButton.gameObject.SetActive(true);
        }
    }

    private bool IsFirstSlide()
    {
        return slideIndex < 1;
    }

    private bool IsLastSlide()
    {
        return slideIndex >= controlsSlides.Length - 1;
    }
}
