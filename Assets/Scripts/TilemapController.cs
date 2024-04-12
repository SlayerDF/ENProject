using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TilemapController : MonoBehaviour
{
    private void Awake()
    {
        var tilemap = GetComponent<UnityEngine.Tilemaps.Tilemap>();
        tilemap.CompressBounds();

        Globals.WorldBounds = tilemap.cellBounds;
    }
}
