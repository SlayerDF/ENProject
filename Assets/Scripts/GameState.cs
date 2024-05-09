using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameState : MonoBehaviour
{
    [SerializeField]
    private LevelGrid levelGrid;
    public LevelGrid LevelGrid => levelGrid;

    [SerializeField]
    private MainCamera mainCamera;
    public MainCamera MainCamera => mainCamera;

    [SerializeField]
    private GameObject playerSpawn;

    [SerializeField]
    private Player playerPrefab;

    [SerializeField]
    private GameObject[] enemySpawns;

    [SerializeField]
    private Enemy enemyPrefab;

    [SerializeField]
    private GameObject ui;
    public GameObject UI => ui;

    private void Start()
    {
        var player = SpawnPlayer();

        InitializeCamera(player.gameObject);

        SpawnEnemies();
    }

    private void InitializeCamera(GameObject target)
    {
        MainCamera.SetCameraBoundsFromWorldBounds(levelGrid.Bounds);

        MainCamera.Target = target;
    }

    private Player SpawnPlayer()
    {
        var player = Instantiate(playerPrefab, playerSpawn.transform.position, playerSpawn.transform.rotation);
        player.GameState = this;

        Destroy(playerSpawn);

        return player;
    }

    private void SpawnEnemies()
    {
        foreach (var spawn in enemySpawns)
        {
            var enemy = Instantiate(enemyPrefab, spawn.transform.position, spawn.transform.rotation);
            enemy.GameState = this;

            Destroy(spawn);
        }

    }
}
