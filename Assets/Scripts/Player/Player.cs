using System;
using TMPro;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    [SerializeField]
    private LevelGrid levelGrid;

    [SerializeField]
    private GameUI gameUI;

    [SerializeField]
    private GameManager gameManager;

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
        gameUI.UpdateHPText(healthSystem.Health, valueIncreased: true, skipAnimation: true);
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

    private void LateUpdate()
    {
        MovementLateUpdate();
    }

    private void OnDied(HealthSystem healthSystem)
    {
        gameObject.SetActive(false);

        Debug.Log("Player has been desactivated.");
    }

    private void OnHealthValueChanged(HealthSystem healthSystem, int value)
    {
        gameUI.UpdateHPText(healthSystem.Health, valueIncreased: value > 0);
        
        if (value < 0)
        {
            animator.SetTrigger("Damaged");

            gameManager.ApplyPlayerDamagedPenalty();
        }
    }

    private void OnDestroy()
    {
        healthSystem.OnDiedEvent -= OnDied;
        healthSystem.OnDamagedEvent -= OnHealthValueChanged;
        healthSystem.OnHealedEvent -= OnHealthValueChanged;
    }
}
