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

    [Header("Visual")]

    [SerializeField]
    GameObject sprite;

    [SerializeField]
    GameObject testSprite;

    [SerializeField]
    bool testMode = false;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        if (testMode)
        {
            sprite.SetActive(false);
            testSprite.SetActive(true);
        }

        MovementAwake();
        HealthAwake();
    }

    private void Start()
    {
        HealthStart();
    }

    private void Update()
    {
        MovementUpdate();
        BombPlacementUpdate();
        InteractionsUpdate();
    }

    private void FixedUpdate()
    {
        MovementFixedUpdate();
    }

    private void LateUpdate()
    {
        MovementLateUpdate();
    }

    private void Desactivate()
    {
        gameObject.SetActive(false);

        Debug.Log("Player has been desactivated.");
    }

    private void OnDestroy()
    {
        HealthOnDestroy();
    }
}
