using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private Button controlsBackButton;

    // State methods
    private void Awake()
    {
        // Main menu events
        startGameButton.onClick.AddListener(ShowLevelsMenu);
        viewControlsButton.onClick.AddListener(ShowControlsMenu);
        exitGameButton.onClick.AddListener(ExitGame);

        // Levels menu events
        levelTestButton.onClick.AddListener(LoadLevelTestScene);
        level1Button.onClick.AddListener(LoadLevel1Scene);
        levelsBackButton.onClick.AddListener(ShowMainMenu);

        // Controls menu events
        controlsBackButton.onClick.AddListener(ShowMainMenu);
    }

    private void OnDisable()
    {
        ShowMainMenu();
    }

    private void OnDestroy()
    {
        // Main menu events
        startGameButton.onClick.RemoveListener(ShowLevelsMenu);
        viewControlsButton.onClick.RemoveListener(ShowControlsMenu);
        exitGameButton.onClick.RemoveListener(ExitGame);

        // Levels menu events
        levelTestButton.onClick.RemoveListener(LoadLevelTestScene);
        level1Button.onClick.RemoveListener(LoadLevel1Scene);
        levelsBackButton.onClick.RemoveListener(ShowMainMenu);

        // Controls menu events
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
}
