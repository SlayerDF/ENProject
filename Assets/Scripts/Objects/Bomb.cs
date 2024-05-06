using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bomb : MonoBehaviour
{
    public GameState GameState { get; set; }

    [Header("Bomb")]
    [SerializeField]
    private float bombLifetimeSeconds = 2f;

    [SerializeField]
    private Collider2D physicsCollider;

    [Header("Explosion")]
    [SerializeField]
    private Explosion explosionPrefab;

    [SerializeField]
    private Sprite explosionAllowedSprite;

    [SerializeField]
    private int explosionRadius = 3;

    [SerializeField, Range(100, 2000)]
    private int explosionSpreadMs = 250;

    private Vector3 bombPosition;
    private Quaternion bombRotation;

    void Start()
    {
        Debug.Log("Bomb has been placed");

        bombPosition = transform.position;
        bombRotation = transform.rotation;

        Destroy(gameObject, bombLifetimeSeconds);
    }

    private async void OnDestroy()
    {
        Debug.Log("Bomb has been destroyed");

        await SpawnExplosions();
    }

    private async UniTask SpawnExplosions()
    {
        var sourceExplosion = InstantiateExplosion(bombPosition, bombRotation);

        var currentWave = new List<Explosion>();

        await UniTask.Delay(explosionSpreadMs);

        for (int j = 0; j < 4; j++)
        {
            sourceExplosion.transform.Rotate(0, 0, 90);
            var explosion = SpawnNextExplosion(sourceExplosion.gameObject);
            if (explosion) currentWave.Add(explosion);
        }

        for (int i = 2; i < explosionRadius; i++)
        {
            if (currentWave.Count == 0) return;

            await UniTask.Delay(explosionSpreadMs);

            var nextWave = new List<Explosion>();

            for (int j = 0; j < currentWave.Count; j++)
            {
                var explosion = SpawnNextExplosion(currentWave[j].gameObject);
                if (explosion) nextWave.Add(explosion);
            }

            currentWave = nextWave;
        }
    }

    private Explosion SpawnNextExplosion(GameObject source)
    {
        var position = source.transform.position;
        var rotation = source.transform.rotation;

        var cellPosition = GameState.LevelTilemap.WorldToCell(position) + Vector3Int.RoundToInt(rotation * Vector3.right);

        if (GameState.LevelTilemap.GetSprite(cellPosition) != explosionAllowedSprite) return null;

        position = GameState.LevelTilemap.CellToWorld(cellPosition) + GameState.LevelTilemap.cellSize / 2;

        return InstantiateExplosion(position, rotation);
    }

    private Explosion InstantiateExplosion(Vector3 position, Quaternion rotation)
    {
        var explosion = Instantiate(explosionPrefab, position, rotation);
        explosion.GameState = GameState;

        return explosion;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<Player>()) return;

        physicsCollider.excludeLayers = 0;
    }
}
