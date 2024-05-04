using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameState : MonoBehaviour
{
    [SerializeField]
    private Tilemap levelTilemap;

    [SerializeField]
    private Camera mainCamera;

    private void Awake()
    {
        levelTilemap.CompressBounds();

        mainCamera.SetCameraBoundsFromWorldBounds(levelTilemap.cellBounds);
    }
}
