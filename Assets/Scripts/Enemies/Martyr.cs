using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Martyr : Enemy
{
    [Header("Spawn bomb on death")]
    [SerializeField]
    private Bomb bombPrefab;

    [SerializeField]
    private int explosionRadius = 2;

    protected override void OnDied(HealthSystem healthSystem)
    {
        SpawnBomb();

        base.OnDied(healthSystem);
    }

    private void SpawnBomb()
    {
        var bomb = Instantiate(bombPrefab, transform.position, transform.rotation);
        bomb.LevelGrid = levelGrid;
        bomb.Type = Bomb.BombType.Alien;
        bomb.ExplosionRadius = explosionRadius;
    }
}
