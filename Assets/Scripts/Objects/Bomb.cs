using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public enum BombType { Chicken, Alien }

    public LevelGrid LevelGrid { get; set; }

    public BombType Type { get => type; set => type = value; }

    public int ExplosionRadius { get => explosionRadius; set => explosionRadius = value; }

    [Header("Bomb")]

    [SerializeField]
    private Collider2D physicsCollider;

    [SerializeField]
    private int bombLifetimeSeconds = 2;

    [SerializeField]
    private int explosionRadius = 3;

    [Header("Bomb Visual")]

    [SerializeField]
    private BombType type;

    [SerializeField]
    private Sprite chickenBombSprite;

    [SerializeField]
    private Sprite alienBombSprite;

    [Header("Explosion")]
    [SerializeField]
    private Explosion explosionPrefab;

    private IEnumerator Start()
    {
        Debug.Log("Bomb has been planted");

        var spriteRenderer = GetComponent<SpriteRenderer>();

        switch (type)
        {
            case BombType.Chicken:
                spriteRenderer.sprite = chickenBombSprite;
                break;
            case BombType.Alien:
                spriteRenderer.sprite = alienBombSprite;
                break;
        }

        yield return new WaitForSeconds(bombLifetimeSeconds);

        SpawnExplosion();
        Destroy(gameObject);
    }

    private void SpawnExplosion()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        explosion.LevelGrid = LevelGrid;
        explosion.Radius = explosionRadius;
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
