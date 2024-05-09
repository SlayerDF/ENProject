using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private HealthSystem healthSystem;

    // Start is called before the first frame update
    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDiedEvent += OnDied;
    }

    private void OnDied(HealthSystem healthSystem)
    {
        Destroy(gameObject);

        Debug.Log("Wall has been destroyed.");
    }

    private void OnDestroy()
    {
        healthSystem.OnDiedEvent -= OnDied;
    }
}
