using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameState GameState { get; set; }

    [SerializeField, Range(0.1f, 5f)]
    private float lifetimeSeconds = 1f;

    private void Start()
    {
        Destroy(gameObject, lifetimeSeconds);
    }

    private void OnDestroy()
    {
        Debug.Log("Explosion has been destroyed");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealthSystem player = collision.gameObject.GetComponent<PlayerHealthSystem>();

        if (player)
        {
            player.Damage(1);
        }
    }
}
