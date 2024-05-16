using Cysharp.Threading.Tasks;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public LevelGrid LevelGrid { get; set; }

    [Header("Bomb")]
    [SerializeField]
    private int bombLifetimeSeconds = 2;

    [SerializeField]
    private Collider2D physicsCollider;

    [Header("Explosion")]
    [SerializeField]
    private Explosion explosionPrefab;

    async void Start()
    {
        Debug.Log("Bomb has been planted");

        await UniTask.Delay(bombLifetimeSeconds * 1000);

        SpawnExplosion();
        Destroy(gameObject);
    }

    private void SpawnExplosion()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion.LevelGrid = LevelGrid;
    }

    private void OnDestroy()
    {
        Debug.Log("Bomb has been destroyed");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<Player>()) return;

        physicsCollider.excludeLayers = 0;
    }
}
