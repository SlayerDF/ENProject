using Cysharp.Threading.Tasks;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameState GameState { get; set; }

    [SerializeField, Range(0.1f, 5f)]
    private float lifetimeSeconds = 1f;

    [SerializeField]
    private int radius = 3;

    [SerializeField, Range(100, 2000)]
    private int spreadTimeMs = 250;

    [SerializeField]
    private Sprite spreadAllowedSprite;

    private int step = 1;
    private bool stopSpreading = false;

    private async void Start()
    {
        Destroy(gameObject, lifetimeSeconds);

        await UniTask.Delay(spreadTimeMs);

        if (step == 1) SpawnCrossExplosions();
        else SpawnNextExplosion();
    }

    private void OnDestroy()
    {
        Debug.Log("Explosion has been destroyed");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            stopSpreading = true;
        }

        if (collision.gameObject.TryGetComponent(out HealthSystem entity))
        {
            entity.Damage(1);
        }
    }

    private void SpawnCrossExplosions()
    {
        for (int j = 0; j < 4; j++)
        {
            transform.Rotate(0, 0, 90);
            SpawnNextExplosion();
        }
    }

    private Explosion SpawnNextExplosion()
    {
        if (stopSpreading || step >= radius) return null;

        var position = transform.position;
        var rotation = transform.rotation;

        var cellPosition = GameState.LevelGrid.Grid.WorldToCell(position) + Vector3Int.RoundToInt(rotation * Vector3.right);

        position = GameState.LevelGrid.Grid.GetCellCenterWorld(cellPosition);

        return InstantiateExplosion(position, rotation);
    }

    private Explosion InstantiateExplosion(Vector3 position, Quaternion rotation)
    {
        var explosion = Instantiate(this, position, rotation);
        explosion.GameState = GameState;
        explosion.step = step + 1;

        return explosion;
    }
}
