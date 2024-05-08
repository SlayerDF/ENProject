using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        GetComponent<HealthSystem>().OnDiedEvent += OnDied;
    }

    private void OnDied(HealthSystem healthSystem)
    {
        Destroy(gameObject);

        Debug.Log("Wall has been destroyed.");
    }
}
