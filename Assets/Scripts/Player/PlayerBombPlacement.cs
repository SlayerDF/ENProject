using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerBombPlacement: MonoBehaviour
{
    [SerializeField]
    private Tilemap levelTilemap;

    [SerializeField]
    private GameObject bombPrefab;

    [SerializeField]
    private KeyCode placeBombKeycode;

    [SerializeField, Range(0.1f, 10f)]
    private float placeCooldown = 1f;

    private float cooldownTimer = 0f;

    private void Update()
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
        var position = levelTilemap.CellToWorld(levelTilemap.WorldToCell(transform.position)) + new Vector3(0.5f, 0.5f);

        Instantiate(bombPrefab, position, transform.rotation);

        cooldownTimer = placeCooldown;
    }
}
