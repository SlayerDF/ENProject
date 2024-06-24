using Cysharp.Threading.Tasks;
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

    [SerializeField]
    private EndingUI endingUI;

    [SerializeField]
    private AudioManager audioManager;

    [SerializeField]
    private int playerDamagedPenalty = 50;

    private int score = 0;

    private bool endGame = false;

    private void Start()
    {
        gameUI.UpdateScoreText(score, valueIncreased: true, skipAnimation: true);

        audioManager.Play("Music", "LevelMusic");
    }

    private void Update()
    {
        if (Input.GetKeyDown(showMainMenuKeyCode) && !endGame)
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

    private async UniTask EndGame()
    {
        await UniTask.WaitForSeconds(1);

        audioManager.Stop("Music");

        endGame = true;
        Time.timeScale = 0;
    }

    public void ChangeScoreBy(int value)
    {
        var newScore = Mathf.Clamp(score + value, 0, int.MaxValue);

        if (newScore != score)
        {
            score = newScore;

            Debug.Log($"Score changed by {value}");

            gameUI.UpdateScoreText(score, valueIncreased: value > 0);
        }
    }

    public void ApplyPlayerDamagedPenalty()
    {
        ChangeScoreBy(-playerDamagedPenalty);
    }

    public async UniTask EndGameWin()
    {
        await EndGame();

        endingUI.ShowWinScreen(score);
    }

    public async UniTask EndGameLose()
    {
        await EndGame();

        endingUI.ShowLoseScreen();
    }
}
