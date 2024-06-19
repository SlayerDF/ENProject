using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected LevelGrid levelGrid;

    [SerializeField]
    protected GameManager gameManager;

    [Header("Movement")]

    [SerializeField, Range(0.1f, 5f)]
    private float movementSpeed = 5;

    [SerializeField, Range(0.1f, 5f)]
    private float randomRotationSeconds = 3;

    [Header("Visual")]

    [SerializeField]
    GameObject sprite;

    [SerializeField]
    GameObject testSprite;

    [SerializeField]
    bool testMode = false;

    [Header("Other")]

    [SerializeField]
    private int killScore = 0;

    private Rigidbody2D rb2d;

    private Animator animator;

    private float lastRotateTime = 0f;

    private Vector2 moveDirection;

    private HealthSystem healthSystem;

    private DropLootSystem dropLootSystem;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        lastRotateTime = Time.time;

        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDiedEvent += OnDied;

        dropLootSystem = GetComponent<DropLootSystem>();

        if (testMode)
        {
            sprite.SetActive(false);
            testSprite.SetActive(true);
        }
    }

    private void Start()
    {
        UpdateMoveDirection(Quaternion.Euler(0, 0, Random.Range(0, 4) * 90) * transform.right);
    }

    private void Update()
    {
        if (Time.time > lastRotateTime + randomRotationSeconds) ChangeDirection();
    }

    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position + movementSpeed * Time.fixedDeltaTime * moveDirection);
    }

    private void LateUpdate()
    {
        animator.SetFloat("XMovement", moveDirection.x);
        animator.SetFloat("YMovement", moveDirection.y);
    }

    protected virtual void OnDied(HealthSystem healthSystem)
    {
        dropLootSystem.DropLoot();
        gameManager.ChangeScoreBy(killScore);
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

            if (Vector2.Angle(moveDirection, contactDirection) < 45) return true;
        }

        return false;
    }

    private void ChangeDirection()
    {
        var cellPosition = levelGrid.Grid.WorldToCell(transform.position);

        transform.position = levelGrid.Grid.GetCellCenterWorld(cellPosition);

        var rotationDirection = Random.value >= 0.5 ? 1 : -1;

        UpdateMoveDirection(Quaternion.Euler(0, 0, rotationDirection * 90) * moveDirection);        

        lastRotateTime = Time.time;
    }

    private void UpdateMoveDirection(Vector2 direction)
    {
        moveDirection = direction;
        testSprite.transform.right = direction;
    }

    private void OnDestroy()
    {
        healthSystem.OnDiedEvent -= OnDied;
    }
}
