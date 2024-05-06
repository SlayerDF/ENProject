using UnityEngine;

public partial class Player : MonoBehaviour
{
    public GameState GameState { get; set; }

    private PlayerHealthSystem healthSystem;

    private void Awake()
    {
        MovementAwake();

        healthSystem = GetComponent<PlayerHealthSystem>();
        healthSystem.OnDiedEvent += OnDied;
    }

    private void OnDied(HealthSystem healthSystem)
    {
        gameObject.SetActive(false);

        Debug.Log("Player has been desactivated.");
    }

    private void Update()
    {
        MovementUpdate();
        BombPlacementUpdate();
    }

    private void FixedUpdate()
    {
        MovementFixedUpdate();
    }
}
