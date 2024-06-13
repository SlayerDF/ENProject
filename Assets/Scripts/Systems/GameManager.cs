using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private KeyCode showMainMenuKeyCode;

    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameUI gameUI;

    private int score = 0;

    private void Start()
    {
        gameUI.UpdateScoreText(score);
    }

    private void Update()
    {
        if (Input.GetKeyDown(showMainMenuKeyCode))
        {
            ToggleMenu();
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }

    private void ToggleMenu()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        Time.timeScale = 1 - Time.timeScale;
    }

    public void IncrementScore(int value)
    {
        score += value;

        Debug.Log($"Score increased by {value}");

        gameUI.UpdateScoreText(score);
    }
}
