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
        healthSystem.OnDamagedEvent += (hs, _) => UpdateHPText(hs.Health);
        healthSystem.OnHealedEvent += (hs, _) => UpdateHPText(hs.Health);
    }

    private void Start()
    {
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

    private void UpdateHPText(int value)
    {
        playerHPText.text = $"Player HP: {value}";
    }
}
