using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameState : MonoBehaviour
{
    [SerializeField]
    private Tilemap levelTilemap;
    public Tilemap LevelTilemap => levelTilemap;

    [SerializeField]
    private Camera mainCamera;
    public Camera MainCamera => mainCamera;

    [SerializeField]
    private GameObject playerSpawn;

    [SerializeField]
    private Player playerPrefab;

    [SerializeField]
    private TextMeshProUGUI debugText;
    public TextMeshProUGUI DebugText => debugText;

    private void Awake()
    {
        var player = SpawnPlayer();

        InitializeCamera(player.gameObject);
    }

    private void InitializeCamera(GameObject target)
    {
        LevelTilemap.CompressBounds();

        MainCamera.SetCameraBoundsFromWorldBounds(LevelTilemap.cellBounds);

        MainCamera.Target = target;
    }

    private Player SpawnPlayer()
    {
        var player = Instantiate(playerPrefab, playerSpawn.transform.position, playerSpawn.transform.rotation);
        player.GameState = this;

        Destroy(playerSpawn);

        return player;
    }
}
