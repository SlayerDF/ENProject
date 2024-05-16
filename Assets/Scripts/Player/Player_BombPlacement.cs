using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public partial class Player
{
    [Header("Bomb Placement")]
    [SerializeField]
    private Bomb bombPrefab;

    [SerializeField]
    private KeyCode placeBombKeycode;

    [SerializeField, Range(0.1f, 10f)]
    private float placeCooldown = 1f;

    private float cooldownTimer = 0f;

    private void BombPlacementUpdate()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else if (Input.GetKey(placeBombKeycode))
        {
            PlaceBomb();
        }
    }

    private void PlaceBomb()
    {
        var cellPosition = levelGrid.Grid.WorldToCell(transform.position);
        var position = levelGrid.Grid.GetCellCenterWorld(cellPosition);

        var bomb = Instantiate(bombPrefab, position, transform.rotation);
        bomb.LevelGrid = levelGrid;

        cooldownTimer = placeCooldown;
    }
}
