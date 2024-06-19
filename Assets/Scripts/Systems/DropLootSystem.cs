using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DropLootSystem : MonoBehaviour
{
    [Serializable]
    public class LootItem
    {
        public GameObject prefab;

        public int fraction;

        [NonSerialized]
        public int position = -1;
    }

    [SerializeField]
    private List<LootItem> lootItems = new();

    private int totalLootFractions;

    private void Awake()
    {
        IndexItems();
    }

    public void AddItem(GameObject prefab, int fraction)
    {
        lootItems.Add(new LootItem { prefab = prefab, fraction = fraction });
        IndexItems();
    }

    private void IndexItems()
    {
        totalLootFractions = lootItems.Sum((item) => item.fraction);

        for (int i = 0; i < lootItems.Count; i++)
        {
            var itemPosition = i > 0 ? lootItems[i - 1].position : 0;
            lootItems[i].position = lootItems[i].fraction + itemPosition;
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
