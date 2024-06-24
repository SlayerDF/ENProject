using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public LevelGrid LevelGrid { get; set; }

    public int Radius { get => radius; set => radius = value; }

    [SerializeField]
    private int radius = 3;

    [SerializeField]
    private float animationSeconds = 1f;

    [SerializeField]
    private GameObject fireSpriteGameobject;

    private Collider2D damageCollider;

    private Animator animator;

    private AudioManager audioManager;

    private int step = 1;
    private bool stopSpreading = false;

    private void Awake()
    {
        damageCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        audioManager = GetComponent<AudioManager>();

        animator.SetFloat("SpeedMultiplier", 1 / animationSeconds);
    }

    private void Start()
    {
        if (step < 1 || step > radius)
        {
            Destroy(gameObject);
        }
        else if (step == 1)
        {
            animator.SetTrigger("Cross");
            audioManager.Play("Default", "Explosion");
        }
        else if (step == radius)
        {
            animator.SetTrigger("End");
            audioManager.Play("CloseProximity", "Fire");
        }
        else
        {
            animator.SetTrigger("Middle");
        }
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

#pragma warning disable IDE0051 // Remove unused private members (because they are called from Animator events)
    private async void OnAnimationEnded()
    {
        damageCollider.enabled = false;

        await audioManager.WaitToFinishAll();

        Destroy(gameObject);
    }

    private void SpawnCrossExplosions()
    {
        for (int j = 0; j < 4; j++)
        {
            transform.Rotate(0, 0, 90);
            fireSpriteGameobject.transform.Rotate(0, 0, -90);
            SpawnNextExplosion();
        }
    }
#pragma warning restore IDE0051 // Remove unused private members

    private Explosion SpawnNextExplosion()
    {
        if (stopSpreading || step >= radius) return null;

        var position = transform.position;
        var rotation = transform.rotation;

        var cellPosition = LevelGrid.Grid.WorldToCell(position) + Vector3Int.RoundToInt(rotation * Vector3.right);

        position = LevelGrid.Grid.GetCellCenterWorld(cellPosition);

        return InstantiateExplosion(position, rotation);
    }

    private Explosion InstantiateExplosion(Vector3 position, Quaternion rotation)
    {
        var explosion = Instantiate(this, position, rotation);
        explosion.LevelGrid = LevelGrid;
        explosion.step = step + 1;

        return explosion;
    }
}
