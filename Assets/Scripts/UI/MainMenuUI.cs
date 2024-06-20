using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // Main menu
    [SerializeField]
    private GameObject mainMenuPanel;

    [SerializeField]
    private Button startGameButton;

    [SerializeField]
    private Button viewControlsButton;

    [SerializeField]
    private Button mainMenuButton;

    [SerializeField]
    private Button exitGameButton;

    // Levels menu
    [SerializeField]
    private GameObject levelsMenuPanel;

    [SerializeField]
    private Button levelTestButton;

    [SerializeField]
    private Button level1Button;

    [SerializeField]
    private Button levelsBackButton;

    // Controls menu
    [SerializeField]
    private GameObject controlsMenuPanel;

    [SerializeField]
    private GameObject[] controlsSlides;

    [SerializeField]
    private Button controlsPrevSlideButton;

    [SerializeField]
    private Button controlsNextSlideButton;

    [SerializeField]
    private Button controlsBackButton;

    private int slideIndex = 0;

    // State methods
    private void Awake()
    {
        // Main menu events
        startGameButton.onClick.AddListener(ShowLevelsMenu);
        viewControlsButton.onClick.AddListener(ShowControlsMenu);
        mainMenuButton.onClick.AddListener(LoadMainMenuScene);
        exitGameButton.onClick.AddListener(ExitGame);

        // Levels menu events
        levelTestButton.onClick.AddListener(LoadLevelTestScene);
        level1Button.onClick.AddListener(LoadLevel1Scene);
        levelsBackButton.onClick.AddListener(ShowMainMenu);

        // Controls menu events
        controlsPrevSlideButton.onClick.AddListener(PrevSlide);
        controlsNextSlideButton.onClick.AddListener(NextSlide);
        controlsBackButton.onClick.AddListener(ShowMainMenu);
    }

    private void OnEnable()
    {
        slideIndex = 0;
        UpdateSlideButtonsVisual();
        ShowMainMenu();
    }

    private void OnDestroy()
    {
        // Main menu events
        startGameButton.onClick.RemoveListener(ShowLevelsMenu);
        viewControlsButton.onClick.RemoveListener(ShowControlsMenu);
        mainMenuButton.onClick.RemoveListener(LoadMainMenuScene);
        exitGameButton.onClick.RemoveListener(ExitGame);

        // Levels menu events
        levelTestButton.onClick.RemoveListener(LoadLevelTestScene);
        level1Button.onClick.RemoveListener(LoadLevel1Scene);
        levelsBackButton.onClick.RemoveListener(ShowMainMenu);

        // Controls menu events
        controlsPrevSlideButton.onClick.RemoveListener(PrevSlide);
        controlsNextSlideButton.onClick.RemoveListener(NextSlide);
        controlsBackButton.onClick.RemoveListener(ShowMainMenu);
    }

    // Switch scene methods
    private void LoadLevelTestScene()
    {
        SceneManager.LoadScene("LevelTest");
    }

    private void LoadLevel1Scene()
    {
        SceneManager.LoadScene("Level1");
    }

    private void LoadMainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void ExitGame()
    {
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
