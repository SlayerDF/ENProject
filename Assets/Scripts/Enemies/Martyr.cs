using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Martyr : Enemy
{

    [Header("Explosion on death")]
    [SerializeField]
    private Explosion explosionPrefab;

    [SerializeField]
    private int explosionRadius = 2;

    protected override void OnDied(HealthSystem healthSystem)
    {
        SpawnExplosion();

        base.OnDied(healthSystem);
    }

    private void SpawnExplosion()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion.LevelGrid = levelGrid;
        explosion.Radius = explosionRadius;
    }
}
