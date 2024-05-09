using UnityEngine;

public class Enemy : MonoBehaviour
{
    // TODO: Revise this approach
    public GameState GameState { get; set; }

    [Header("Movement")]
    [SerializeField, Range(0.1f, 5f)]
    private float movementSpeed = 5;

    [SerializeField, Range(0.1f, 5f)]
    private float RandomRotationSeconds = 3;

    private Rigidbody2D rb2d;

    private float lastRotateTime = 0f;

    private HealthSystem healthSystem;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        lastRotateTime= Time.time;

        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDiedEvent += OnDied;
    }

    private void Update()
    {
        if (Time.time > lastRotateTime + RandomRotationSeconds) ChangeDirection();
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition((Vector3)rb2d.position + movementSpeed * Time.fixedDeltaTime * transform.right);
    }

    private void OnDied(HealthSystem healthSystem)
    {
        Destroy(gameObject);

        Debug.Log("Enemy has been destroyed.");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealthSystem player))
        {
            player.Damage(1);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (IsStuck(collision)) ChangeDirection();
    }

    private bool IsStuck(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Obstacle")) return false;

        var contacts = new ContactPoint2D[collision.contactCount];
        int contactsNum = collision.GetContacts(contacts);

        for (int i = 0; i < contactsNum; i++)
        {
            var contactDirection = (contacts[i].point - (Vector2)transform.position).normalized;

            if (Vector2.Angle(transform.right, contactDirection) < 45) return true;
        }

        return false;
    }

    private void ChangeDirection()
    {
        var cellPosition = GameState.LevelGrid.Grid.WorldToCell(transform.position);

        transform.position = GameState.LevelGrid.Grid.GetCellCenterWorld(cellPosition);

        var rotationDirection = Random.value >= 0.5 ? 1 : -1;

        transform.Rotate(0, 0, rotationDirection * 90);

        lastRotateTime = Time.time;
    }

    private void OnDestroy()
    {
        healthSystem.OnDiedEvent -= OnDied;
    }
}
