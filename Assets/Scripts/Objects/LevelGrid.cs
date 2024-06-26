using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGrid : MonoBehaviour
{
    public Grid Grid { get; private set; }

    private void Awake()
    {
        Grid = GetComponent<Grid>();
    }

    public BoundsInt CalculateBounds()
    {
        var bounds = new BoundsInt();

        foreach (var tilemap in GetComponentsInChildren<Tilemap>())
        {
            tilemap.CompressBounds();

            if (tilemap.cellBounds.xMin < bounds.xMin) bounds.xMin = tilemap.cellBounds.xMin;
            if (tilemap.cellBounds.xMax > bounds.xMax) bounds.xMax = tilemap.cellBounds.xMax;
            if (tilemap.cellBounds.yMin < bounds.yMin) bounds.yMin = tilemap.cellBounds.yMin;
            if (tilemap.cellBounds.yMax > bounds.yMax) bounds.yMax = tilemap.cellBounds.yMax;
        }

        return bounds;
    }
}
