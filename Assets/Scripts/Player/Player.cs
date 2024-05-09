using System;
using TMPro;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    public GameState GameState { get; set; }

    private TextMeshProUGUI playerHPText;

    private PlayerHealthSystem healthSystem;

    private void Awake()
    {
        MovementAwake();

        healthSystem = GetComponent<PlayerHealthSystem>();
        healthSystem.OnDiedEvent += OnDied;
        healthSystem.OnDamagedEvent += OnHealthValueChanged;
        healthSystem.OnHealedEvent += OnHealthValueChanged;
    }

    private void Start()
    {
        // TODO: Need to rework.
        playerHPText = GameState.UI.transform.Find("PlayerHP").GetComponent<TextMeshProUGUI>();
        UpdateHPText(healthSystem.Health);
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

    private void OnDied(HealthSystem healthSystem)
    {
        gameObject.SetActive(false);

        Debug.Log("Player has been desactivated.");
    }

    private void OnHealthValueChanged(HealthSystem healthSystem, int value)
    {
        UpdateHPText(healthSystem.Health);
    }

    private void UpdateHPText(int value)
    {
        playerHPText.text = $"Player HP: {value}";
    }

    private void OnDestroy()
    {
        healthSystem.OnDiedEvent -= OnDied;
        healthSystem.OnDamagedEvent -= OnHealthValueChanged;
        healthSystem.OnHealedEvent -= OnHealthValueChanged;
    }
}
