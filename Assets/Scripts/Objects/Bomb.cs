using UnityEngine;

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

    private Vector3 bombPosition;
    private Quaternion bombRotation;

    void Start()
    {
        Debug.Log("Bomb has been placed");

        bombPosition = transform.position;
        bombRotation = transform.rotation;

        Destroy(gameObject, bombLifetimeSeconds);
    }

    private void OnDestroy()
    {
        Debug.Log("Bomb has been destroyed");

        var explosion = Instantiate(explosionPrefab, bombPosition, bombRotation);
        explosion.GameState = GameState;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.GetComponent<Player>()) return;

        physicsCollider.excludeLayers = 0;
    }
}
