using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DropLootSystem : MonoBehaviour
{
    [Serializable]
    private class LootItem
    {
        public GameObject prefab;

        public int fraction;

        [NonSerialized]
        public int position = -1;
    }

    [SerializeField]
    private LootItem[] lootItems;

    private int totalLootFractions;

    private void Awake()
    {
        totalLootFractions = lootItems.Sum((item) => item.fraction);

        for (int i = 0; i < lootItems.Length; i++)
        {
            lootItems[i].position = lootItems[i].fraction + (i > 0 ? lootItems[i - 1].position : 0);
        }
    }

    public void DropLoot()
    {
        var sample = UnityEngine.Random.Range(0, totalLootFractions);
        var item = lootItems.First((pair) => sample < pair.position);

        if (item.prefab != null)
        {
            Debug.Log($"{name} dropped {item.prefab.name} ({Mathf.RoundToInt((float)item.fraction / totalLootFractions * 100)}%)");

            Instantiate(item.prefab, transform.position, transform.rotation);
        }
    }
}
