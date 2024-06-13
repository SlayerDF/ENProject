using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private KeyCode showMainMenuKeyCode;

    [SerializeField]
    private GameObject mainMenu;

    private void Update()
    {
        if (Input.GetKeyDown(showMainMenuKeyCode))
        {
            ToggleMenu();
        }
    }

    private void ToggleMenu()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        Time.timeScale = 1 - Time.timeScale;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }
}
