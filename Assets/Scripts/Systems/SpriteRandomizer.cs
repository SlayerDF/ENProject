using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomizer : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;

    private void Start()
    {
        if (sprites == null) return;

        var sprite = sprites[Random.Range(0, sprites.Length)];

        GetComponent<SpriteRenderer>().sprite = sprite;
    }
}
